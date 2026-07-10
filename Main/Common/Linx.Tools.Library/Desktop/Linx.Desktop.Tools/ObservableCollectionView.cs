using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Linx.Tools
{
    public interface IBindingCollectionView : ICollectionView
    {
        object AddNew();
        int Count { get; }
        void RemoveAt(int index);
        Type GetItemType();
    }
	
    public class ObservableCollectionView<T> : ObservableCollection<T>, IBindingCollectionView where T : class
    {
        public ObservableCollectionView() : base() { }

        /// <summary>
        /// Add new item and return the same.
        /// </summary>
        /// <returns></returns>
        public object AddNew()
        {
            //Create new instance
            T newItem = (T)Activator.CreateInstance(typeof(T));

            //Add to list
            this.Add(newItem);

            //Return new item
            return newItem;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            //Adjust new position
            if (e.Action == NotifyCollectionChangedAction.Add && !e.NewItems.IsNull() && e.NewItems.Count > 0)
                this.UpdateCurrentIndex(this.Count - 1);
            else if (e.Action == NotifyCollectionChangedAction.Remove && !e.OldItems.IsNull() && e.OldItems.Count > 0 && this.Count > 0 && this._currentIndex >= 0)
            {
                if (this._currentIndex < this.Count)
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentItem"));
                else
                    this.UpdateCurrentIndex(this.Count - 1); 
            }
            else
                this.Refresh();
        }


        //Get the item type.
        public virtual System.Type GetItemType()
        {
            return typeof(T);
        }

        public ObservableCollectionView(IEnumerable<T> items)
        {
            if (!items.IsNull())
            {
                foreach (T item in items)
                {
                    this.Add(item);
                }

				if (this.Count > 0)
					this.UpdateCurrentIndex(0);				
            }
        }

        private int _currentIndex = -1;

        // This should really be Predicate<T> but the ICollectionView
        // interface defines the filter predicate with an object type parameter.
        private Predicate<object> _filter;

        #region ICollectionView Members

        public event EventHandler CurrentChanged;

        public event CurrentChangingEventHandler CurrentChanging;

        public bool Contains(object item)
        {
            return base.Contains(item as T);
        }

        public object CurrentItem
        {
            get { return _currentIndex == -1 ? null : this[_currentIndex]; }
        }

        public int CurrentPosition
        {
            get { return _currentIndex; }
        }

        public bool IsCurrentAfterLast
        {
            get { return _currentIndex >= this.Count; }
        }

        public bool IsCurrentBeforeFirst
        {
            get { return _currentIndex < 0; }
        }

        public bool IsEmpty
        {
            get { return this.Count == 0; }
        }

        public bool MoveCurrentTo(object item)
        {
            if (this.Contains(item))
            {
                return UpdateCurrentIndex(this.IndexOf(item as T));
            }

            // If item is not in collection or is null, move to unselected state.
            return UpdateCurrentIndex(-1);
        }

        public bool MoveCurrentToFirst()
        {
            return UpdateCurrentIndex(0);
        }

        public bool MoveCurrentToLast()
        {
            return UpdateCurrentIndex(this.Count - 1);
        }

        public bool MoveCurrentToNext()
        {
            return UpdateCurrentIndex(_currentIndex + 1);
        }

        public bool MoveCurrentToPosition(int position)
        {
            return UpdateCurrentIndex(position);
        }

        public bool MoveCurrentToPrevious()
        {
            return UpdateCurrentIndex(_currentIndex > 0 ? _currentIndex - 1 : _currentIndex);
        }

        public void Refresh()
        {
            if (this.Count > 0)
            {
                if (_currentIndex < 0 || _currentIndex > (this.Count - 1))
                    UpdateCurrentIndex(0);
            }
            else if (_currentIndex != -1)
                UpdateCurrentIndex(-1);
        }

        public IEnumerable SourceCollection
        {
            get { return this; }
        }

        public bool CanFilter
        {
            get { return false; }
        }

        public Predicate<object> Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        public bool CanSort
        {
            get { return false; }
        }

        public SortDescriptionCollection SortDescriptions
        {
            get
            {
                return null;
            }
        }

        public bool CanGroup
        {
            get { return false; }
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get
            {
                return null;
            }
        }

        public ReadOnlyObservableCollection<object> Groups
        {
			get { return null; }
        }

        public IDisposable DeferRefresh()
        {
			return null;
        }

        public System.Globalization.CultureInfo Culture
        {
            get
            {
                return System.Globalization.CultureInfo.InvariantCulture;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Commands - SelectNext, SelectPrevious, SortBy, GroupBy
        public DelegateCommand<object> SelectNextCommand { get; private set; }

        private bool CanSelectNext(object arg)
        {
            return (_currentIndex < this.Count - 1);
        }

        private void SelectNext(object obj)
        {
            MoveCurrentToNext();
        }

        public DelegateCommand<object> SelectPreviousCommand { get; private set; }
        private bool CanSelectPrevious(object arg)
        {
            return (_currentIndex > 0);
        }

        private void SelectPrevious(object obj)
        {
            MoveCurrentToPrevious();
        }

        public DelegateCommand<string> SortByCommand { get; private set; }

        private void SortBy(string property)
        {
            if (!this.SortDescriptions.IsNull())
            {
                this.SortDescriptions.Clear();
                SortDescriptions.Add(new SortDescription(property, ListSortDirection.Ascending));
            }
        }

        public DelegateCommand<string> GroupByCommand { get; private set; }

        private void GroupBy(string property)
        {
            if (!this.GroupDescriptions.IsNull())
            {
                this.GroupDescriptions.Clear();
                GroupDescriptions.Add(new PropertyGroupDescription(property));
            }
        }
        #endregion

        private object GetPropertyValue(T item, string propertyName)
        {
            PropertyInfo pi = item.GetType().GetProperty(propertyName);
            if (pi != null)
            {
                return pi.GetValue(item, null);
            }
            return null;
        }
				

        private bool UpdateCurrentIndex(int index)
        {
            // Calculate new index bounded by -1 and the current collection size.
            int newIndex;
            newIndex = System.Math.Max(index, -1);
            newIndex = System.Math.Min(newIndex, this.Count - 1);

            if (_currentIndex != newIndex)
            {
                if (this.CurrentChanging != null)
                    this.CurrentChanging(this, new CurrentChangingEventArgs(false));

                _currentIndex = newIndex;

				if (this.CurrentChanged != null)
					this.CurrentChanged(this, new EventArgs());

				
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentPosition"));
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentItem"));
                if (!this.SelectNextCommand.IsNull())
                    SelectNextCommand.RaiseCanExecuteChanged();
                if (!this.SelectPreviousCommand.IsNull())
                    SelectPreviousCommand.RaiseCanExecuteChanged();
            }

            return _currentIndex != -1;
        }
    }

    public class PropertyGroupDescription : GroupDescription
    {
        public string PropertyName { get; private set; }

        public PropertyGroupDescription(string propertyName)
        {
            PropertyName = propertyName;
        }

        public override object GroupNameFromItem(object item, int level, System.Globalization.CultureInfo culture)
        {
            PropertyInfo pi = item.GetType().GetProperty(PropertyName);
            if (pi != null)
            {
                return pi.GetValue(item, null);
            }
            return null;
        }
    }

}
