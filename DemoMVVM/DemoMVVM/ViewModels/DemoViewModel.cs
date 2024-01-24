using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoMVVM.ViewModels
{
    class DemoViewModel
    {
        private RelayCommand submitCommand;

        private string content;

        public string Content {
            get { return content; }
            set
            {
                content = value;
            }
        }
        public RelayCommand SubmitCommand { get => submitCommand; set => submitCommand = value; }

        public DemoViewModel() {
            //gan SubmitCommand den 1 funtion
            SubmitCommand = new RelayCommand(OnSubmit);
        }

        private void OnSubmit()//xu ly cho SubmitCommand
        {
            Content = "Da bam";
            Console.WriteLine("Da bam submit");
        }

    }
}
