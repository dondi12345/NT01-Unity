using System;

public class NumberForm
{
    public static string ToString(float number)
    {
        float num;
        if(number > 1000000000000){
            num =(number/1000000000000);
            return num.ToString("F2")+"Tri";
        }
        if(number > 1000000000){
            num = (number/1000000000);
            return num.ToString("F2") +"B";
        }
        if(number > 1000000){
            num = (number/1000000);
            return num.ToString("F2") +"M";
        }
        if(number > 1000){
            num = (number/1000);
            return num.ToString("F2") +"K";
        }
        return (int)number +"";
    }

    public static string MinimunToString(float number){
        float num;
        if(number > 1000000000000){
            num =(number/1000000000000);
            return num.ToString("F2")+"Tri";
        }
        if(number > 1000000000){
            num = (number/1000000000);
            return num.ToString("F2") +"B";
        }
        if(number > 1000000){
            num = (number/1000000);
            return num.ToString("F2") +"M";
        }
        if(number > 1000){
            num = (number/1000);
            return num.ToString("F2") +"K";
        }
        return number.ToString("F2");
    }
}
