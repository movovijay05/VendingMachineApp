using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineApp.Models
{
    public class GenericFunctions
    {
      public bool checkIfTwoStringDoubleDictionariesAreIdenticalWithoutSorting(
      Dictionary<string, double> oldDict,
      Dictionary<string, double> newDict)
        {
            // Simple check, are the counts the same?
            if (!oldDict.Count.Equals(newDict.Count)) return false;

            // iterate through all the keys in oldDict and
            // verify whether the key exists in the newDict
            for (int key = 0; key < oldDict.Count; key++)
            {
                if (newDict.ElementAt(key).Equals(oldDict.ElementAt(key)))
                {
                    // iterate through each value for the current key in oldDict and 
                    // verify whether or not it exists for the current key in the newDict
                }
                else { return false; }
            }
            return true;
        }
        public bool checkIfTwoStringIntDictionariesAreIdenticalWithoutSorting(
      Dictionary<string, int> oldDict,
      Dictionary<string, int> newDict)
        {
            // Simple check, are the counts the same?
            if (!oldDict.Count.Equals(newDict.Count)) return false;

            // iterate through all the keys in oldDict and
            // verify whether the key exists in the newDict
            for (int key = 0; key < oldDict.Count; key++)
            {
                if (newDict.ElementAt(key).Equals(oldDict.ElementAt(key)))
                {
                    // iterate through each value for the current key in oldDict and 
                    // verify whether or not it exists for the current key in the newDict
                }
                else { return false; }
            }
            return true;
        }
    }
}