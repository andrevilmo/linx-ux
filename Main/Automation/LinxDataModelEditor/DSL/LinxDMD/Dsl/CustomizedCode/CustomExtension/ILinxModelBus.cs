using Microsoft.VisualStudio.Modeling.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DslIntegration = global::Microsoft.VisualStudio.Modeling.Integration;

namespace Linx.BusinessDataModelDesigner
{
    public interface ILinxModelBus
    {        
        ModelBusAdapter CreateNewAdapter(ModelBusReference reference, IServiceProvider serviceProvider);
        void UpdateModelBus(IModelBus modelBus);
    }
}
