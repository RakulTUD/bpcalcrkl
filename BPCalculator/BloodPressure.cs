﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Ideal Blood Pressure")]  Ideal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High,
        [Display(Name = "Diastolic Value should be between 40 to 59")] LowDiastolic,
        [Display(Name = "Diastolic Value should be between 60 to 79")] IdealDiastolic,
        [Display(Name = "Diastolic Value should be between 80 to 89")] PreHighDiastolic,
        [Display(Name = "Diastolic Value should be between 90 to 100")] HighDiastolic,
        [Display(Name = "")] Empty
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      


        // calculate BP category
        public BPCategory Category
        {
            get
            {
            
                if(((Systolic >=70 && Systolic <90) && !(Diastolic >=40 && Diastolic <60))) {
                    return BPCategory.LowDiastolic;
                }  else if(((Systolic >=90 && Systolic <120) && !(Diastolic >=60 && Diastolic <80))){
                    return BPCategory.IdealDiastolic;
                }else if(((Systolic >=120 && Systolic <140) && !(Diastolic >=80 && Diastolic <90))){
                   return  BPCategory.PreHighDiastolic;
                }else if(((Systolic >=140 && Systolic <=190) && !(Diastolic >=90 && Diastolic <=100))){
                    return BPCategory.HighDiastolic;
                } else if(((Systolic >=70 && Systolic <90) && (Diastolic >=40 && Diastolic <60)) || (Systolic >=70 && Systolic <90)){
                    return BPCategory.Low;
                } else if(((Systolic >=90 && Systolic <120) && (Diastolic >=60 && Diastolic <80)) || (Systolic >90 && Systolic <120)){
                    return BPCategory.Ideal;
                }else if(((Systolic >=120 && Systolic <140) && (Diastolic >=80 && Diastolic <90)) || (Systolic >120 && Systolic <140)){
                   return  BPCategory.PreHigh;
                }else if(((Systolic >=140 && Systolic <=190) && (Diastolic >=90 && Diastolic <=100)) || (Systolic >140 && Systolic <=190)){
                    return BPCategory.High;
                }
                return BPCategory.Empty;


            }
        }
    }
}
