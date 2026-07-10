using Microsoft.VisualStudio.Modeling;



namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterDesignerRoot : IAditionalInformation
    {
        private void CheckVersion()
        {
            string version = "1.0.0.220";
            if (this.Version != version)
            {
                using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Changing Version."))
                {
                    this.Version = version;
                    this.HasStructuralChanges = true;
                    transaction.Commit();
                }
            }
            else if (IsAutomaticSaving || !IsMainWindowVisible())
            {
                this.HasStructuralChanges = true;
            }
        }

        public string GetDistributorProductName()
        {
            return "Linx Framework 6.0.0";
        }
    }
}
