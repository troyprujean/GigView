using System;

namespace GigView.Models
{
    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }

        public ErrorViewModel(Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}