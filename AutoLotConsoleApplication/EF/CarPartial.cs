using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotConsoleApplication.EF
{
    public partial class Car
    {
        public override string ToString()
        {
            //Т.к. столбец PetName может быть пустым, предоставить стандартное имя "NoName"
            return $"{this.CarNickName ?? "**No Name**"} is a {this.Color} {this.Make} with ID {this.CarId}.";
        }
    }
}
