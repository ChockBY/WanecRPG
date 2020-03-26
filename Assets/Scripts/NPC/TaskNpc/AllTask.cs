using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AllTask
{
    public string AwardImage;
    public int Count;
    public int id;
    public AllTask(int id,string _award,int _count)
    {
        this.id = id;
        AwardImage = _award;
        Count = _count;
    }
}

