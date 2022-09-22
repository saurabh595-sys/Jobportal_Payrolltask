﻿using JobPortal.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.ForgetPassword
{
   public interface IOtpService
    {
        Task<Otp> Add(Otp entity);
        Task<Otp> Validate(int otp);
        Task<bool> IsOtpUnique(int otp);
    }
}
