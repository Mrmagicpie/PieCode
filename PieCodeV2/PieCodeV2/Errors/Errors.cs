using System;

namespace PieCodeV2.Errors
{
    public class Errors
    {}
    
    public class IsThiccException : Exception
    {}
    
    public class FileNotFound : IsThiccException
    {}
    
    public class FileNotValid : IsThiccException
    {}
}