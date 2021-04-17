using System;

namespace PieCodeV2.Errors
{
    public class Errors
    {}
    
    public class PieCodeException : Exception
    {}
    
    public class FileNotFound : PieCodeException
    {}
    
    public class FileNotValid : PieCodeException
    {}
    
    public class SyntaxError : PieCodeException
    {}
}