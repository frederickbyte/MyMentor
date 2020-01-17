using System;
using System.IO;

// This is a class to throw all the random functions you want in that don't belong anywhere else
public static class Utilities
{
    // Gets the parent of the parent  of the current directory, returned as a string
   public static string Get_grandparent_path()
    {
        string path = System.IO.Directory.GetCurrentDirectory();
        string grandparentDir = System.IO.Directory.GetParent(path).Parent.FullName;
        return grandparentDir;
    }
}
