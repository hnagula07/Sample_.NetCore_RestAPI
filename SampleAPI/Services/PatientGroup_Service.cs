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
        int ColumnsCount=0;
        public Patient CalculatePatientGroups(PatientData pp){
             
           ColumnsCount = pp.matrix[0].Count;
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
                      if (ValidatePatients(item,row,column,pp)) {
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

        private bool ValidatePatients(int patient,int row,int column,PatientData pp){
            if(patient == 0){
                return false;
            }
            Visited.Add(row.ToString()+column.ToString());
            List<string> coOrdinates = new List<string>();
    
            if(row==0 && column>0){
                if(CheckForSpecialCase(row,column,pp)){
                    return false;
                }
                coOrdinates.Add(row.ToString()+(column-1).ToString());
            }else if(row>0){
            coOrdinates.Add((row-1).ToString()+(column-1).ToString()); //can skip
            coOrdinates.Add((row-1).ToString()+(column).ToString());
            coOrdinates.Add((row-1).ToString()+(column+1).ToString()); //can skip
            }else if(column>0){
                coOrdinates.Add(row.ToString()+(column-1).ToString());
            }

            foreach(var index in coOrdinates){
                if(Visited.Contains(index)){
                    return false;
                }
            }
            return true;
        }
        
        private bool CheckForSpecialCase(int row,int column,PatientData pp){
            if(Visited.Contains(row.ToString()+(column-2).ToString())){
                    if(row+1 <= pp.matrix.Count && column-1 >=0 && pp.matrix[row+1][column-1] == 1){
                        return true;
                    }
            }
            return false;
        }
    }
}