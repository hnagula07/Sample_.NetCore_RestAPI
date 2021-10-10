using System;
using System.Net;
using System.Collections.Generic;
using SampleAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Services
{
    public class PatientGroup_Service
    {
        
        HashSet<string> Visited = new HashSet<string>();
        public Patient CalculatePatientGroups(PatientData pp){
             
            int columnsCount= pp.matrix[0].Count;
            int row = 0;
            int result = 0;
            foreach (var itemArrays in pp.matrix)
            {
                int column = 0;   
                Console.WriteLine(itemArrays);
            //    if(columnsCount != itemArrays.Count)
            //      return ((int)HttpStatusCode.InternalServerError, "Invalid request body");

                
                foreach (var item in itemArrays)
                {
                      if (ValidatePatients(item,row,column)) {
                          Visited.Add(row.ToString()+column.ToString());
                          result++;
                      }
                 
                    column++;
                }
                row++;
                
            }
            Patient p = new Patient();
            p.numberOfGroups = result;
            return p;
            
        }

        private bool ValidatePatients(int patient,int row,int column){
            if(patient == 0){
                return false;
            }
            List<int> coOrdinates = new List<int>();
            // Conditions
            if(row==0){

            }

            if(Visited.Contains(row.ToString()+column.ToString())){

            }

            return true;
        }
        
    }
}