
namespace System
{
    /// <summary>
    /// String扩展类
    /// </summary>
    public static  class EnumExtends
    {
        /// <summary>
        /// 把枚举字符串转成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumstr"></param>
        /// <returns></returns>
        public static  T ToEnum<T>( this object obj)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), obj.ToString(), true);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
       
    }
}

