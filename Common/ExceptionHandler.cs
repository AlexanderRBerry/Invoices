using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exception_Handler
{
    /// <summary>
    /// Prints accumulated error data like a call stack
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        public static void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}

/****************Examples*****************************
  
*************** Top level method *********************
    try
        {
            Code
        }
    catch (Exception ex)
        {
            //This a top level method so we want to handle the exception
            ExceptionHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                        MethodInfo.GetCurrentMethod().Name, ex.Message);
        }

  ************* Lower level method *******************
  try
     {
        Code 
     }
  catch (Exception ex)
    {
        //Throw an exception
        throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
    }
******************************************************* 


/*************************
 *                       *
 * Chapter 13 Exceptions *
 *                       *
 *************************/
