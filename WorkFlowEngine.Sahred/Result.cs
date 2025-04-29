using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowEngine.Shared
{
    public class Result
    {
        public List<KeyValuePair<string, string>> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public Result(bool isSuccess)
        { 
            IsSuccess = isSuccess;
            Errors = new List<KeyValuePair<string, string>>();
        }
        public Result(bool isSuccess, List<KeyValuePair<string,string>> errors):this(isSuccess)
        {
            Errors = errors;
        } 
    }
    public class Result<TValue>:Result
    {
        private readonly TValue? _value;
        public Result(bool isSuccess ,List<KeyValuePair<string,string>> errors):base(isSuccess,errors)
        { 
        }
        public Result(TValue? value, bool isSuccess):base(isSuccess)=>
            _value = value;   
        
        public Result(TValue? value, bool isSuccess, List<KeyValuePair<string, string>> errors) : base(isSuccess, errors) => _value = value;
        
        public TValue Value => IsSuccess? _value!: throw new InvalidOperationException("The value of a failure result can not be accessed.");
         public static implicit operator Result<TValue>(TValue? value) => value;
    } 
}
