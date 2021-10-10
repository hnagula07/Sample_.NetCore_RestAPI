using System.Collections.Generic;
namespace SampleAPI.Models
{
    public class Patient
    {
       public int numberOfGroups{get;set;}
    }

    public class PatientData
    {
        public List<List<int>> matrix{get;set;}
    }
}