using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRegister
    {
        int ProductId { get; set; }
        DateTime NewRegister { get; set; }
    }

    public enum InquestType
    {
        Telephonic, InPerson
    }

    public enum ProductType
    {
        Microbiology, ClinicChemistry
    }

    public enum ProductState
    {
        Normal, CecmedPresent, NoPresent
    }

    public enum WailState
    {
        Authorized, NotAuthorized, OpportunityOfAdvance, PreventionAction
    }
}
