﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    public class PhoneTranslator
    {
        string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "22233344455566677778889999";

        public string ToNumber(string alfaNumericPhoneNumber)
        {
            var NumericPhoneNumber = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(alfaNumericPhoneNumber))
            {
                alfaNumericPhoneNumber = alfaNumericPhoneNumber.ToUpper();

                foreach(var c in alfaNumericPhoneNumber)
                {
                    if ("0123456789".Contains(c))
                    {
                        NumericPhoneNumber.Append(c);
                    }
                    else
                    {
                        var Index = Letters.IndexOf(c);
                        
                        if (Index >= 0)
                        {
                            NumericPhoneNumber.Append(numbers[Index]);
                        }
                    }
                }
            }

            return NumericPhoneNumber.ToString();
        }
    }
}