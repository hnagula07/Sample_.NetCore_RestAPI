using System;
using System.Net;
using System.Collections.Generic;
using SampleAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Services
{
    public class PatientGroup_Service
    {

        Dictionary<string,int> Visited = new Dictionary<string,int>();
        Dictionary<int,List<string>> NodesGroup = new Dictionary<int,List<string>>();
        int ColumnsCount=0;
        public Patient CalculatePatientGroups(PatientData pp){
             
           ColumnsCount = pp.matrix[0].Count;
            int row = 0;
            int result = 1;
            foreach (var itemArrays in pp.matrix)
            {
                int column = 0;   
                Console.WriteLine(itemArrays);
            //    if(columnsCount != itemArrays.Count)
            //      return ((int)HttpStatusCode.InternalServerError, "Invalid request body");

                
                foreach (var item in itemArrays)
                {
                    int res = ValidatePatients(item,row,column,pp);
                    var indexF= row.ToString()+column.ToString();
                    if(result == 1 && res == 1){
                        Visited.Add(indexF,result);
                        NodesGroup.Add(result,new List<string>{indexF});
                    } 
                    // else{
                    //      result = result + res; 
                    //       if(!Visited.ContainsKey(indexF))
                    //       Visited.Add(indexF,result);
                    //        if(!NodesGroup.ContainsKey(result))
                    //       NodesGroup.Add(result,new List<string>{indexF});
                    // }
                    column++;
                }
                row++;
                
            }
            Patient p = new Patient();
            p.numberOfGroups = result;
            return p;
            
        }

        private int ValidatePatients(int patient,int row,int column,PatientData pp){
            int resultCount = 0;
            if(patient == 0){
                return resultCount;
            }
            //Visited.Add(row.ToString()+column.ToString(),0);
            resultCount = 1;
            List<string> coOrdinates = new List<string>();
    
            if(row==0 && column>0){
                // if(CheckForSpecialCase(row,column,pp)){
                //     return resultCount;
                // }
                coOrdinates.Add(row.ToString()+(column-1).ToString());
            }else if(row>0){
                coOrdinates.Add((row-1).ToString()+(column-1).ToString()); //can skip
                coOrdinates.Add((row-1).ToString()+(column).ToString());
                coOrdinates.Add((row-1).ToString()+(column+1).ToString()); //can skip
            }else if(column>0){
                coOrdinates.Add(row.ToString()+(column-1).ToString());
            }

            HashSet<int> paths = new HashSet<int>();

            foreach(var index in coOrdinates){
                if(Visited.ContainsKey(index)){
                    int rank = Visited[index];
                    string Newindex = row.ToString()+column.ToString();
                    Visited[Newindex] = rank;
                    var l = NodesGroup[rank];
                    l.Add(Newindex);
                    NodesGroup[rank] =  l;
                   paths.Add(rank);
                }
            }
            if (paths.Count > 1){
                int topRank = 0;
                foreach (var item in paths)
                {
                    if(topRank==0){
                        topRank = item;
                    }else{
                        foreach (var key in NodesGroup[item])
                        {
                            Visited[key] = topRank;
                        }
                        var ll = NodesGroup[topRank];
                        ll.AddRange(NodesGroup[item]);
                        NodesGroup[topRank] = ll;
                            NodesGroup.Remove(item);
                    }
                }
                return -(paths.Count-1);
            }
            return resultCount;
        }
        
        // private bool CheckForSpecialCase(int row,int column,PatientData pp){
        //     if(Visited.Contains(row.ToString()+(column-2).ToString())){
        //             if(row+1 <= pp.matrix.Count && column-1 >=0 && pp.matrix[row+1][column-1] == 1){
        //                 return true;
        //             }
        //     }else if(Visited.Contains(row.ToString()+(column-2).ToString())){
        //             if(row+1 <= pp.matrix.Count && column-1 >=0 && pp.matrix[row+1][column-1] == 1){
        //                 return true;
        //             }
        //     return false;
        //     }
        //     return true;
        //  }
    }
}


// public Patient CalculatePatientGroups(PatientData pp)
//         {
             
//            List<decimal> buffer = new List<decimal>();
//             foreach (var itemArrays in pp.matrix)
//             {
//                 if(buffer.Count == 0)
//                 {
//                     buffer = itemArray;
//                 }else
//                 {      
//                     int index = 0;    
//                   foreach (var item in itemArray)
//                    {
//                        if(item == 1)
//                        {
//                         if(buffer[index] == 1 || buffer[index] == 0.5)
//                          {
//                             buffer[index] = 1
//                          } else if(buffer[index] == 0)
//                          {
//                              buffer[index] = 0.5
//                          } 
//                        } else{
//                             if(buffer[index] == 1)
//                             {
//                                 buffer[index] = 1
//                             }else if(buffer[index] == 0 || buffer[index] == 0.5)
//                             {
//                                 buffer[index] = 0.5                                              
//                             } 
//                        }
                       
//                         index++;
//                     }

//                 }
                
//             }
//             Patient p = new Patient();
//             p.numberOfGroups = result;
//             return p;
            
//         }