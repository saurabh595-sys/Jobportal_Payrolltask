using System;

namespace JobPortal.Model.Model
{
    public class Otp
    {
        public  int Id { get; set; }
        public int OtpNumber { get; set; }
         public DateTime expiry { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }

    }
}
