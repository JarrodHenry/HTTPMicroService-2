using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTPMicroService.Models
{
    public class CCResponseModel
    {
        public int number { get; set; }
        public string text { get; set; }
        public char letter { get; set; }

        public bool boolresult { get; set; }

        public CCResponseModel()
        {
            this.number = 5;
            this.text = "Hello world";
            this.letter = 'z';
            this.boolresult = false;
        }

        public CCResponseModel(int number, string text, char letter, bool boolresult)
        {
            this.number = number;
            this.text = text;
            this.letter = letter;
            this.boolresult = boolresult;
        }
        
    }
}