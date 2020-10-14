
namespace System
{
    /// <summary>
    /// String扩展类
    /// </summary>
    public static  class StringEx
    {
        /// <summary>
        /// 把枚举字符串转成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumstr"></param>
        /// <returns></returns>
        public static  T ToEnum<T>( this string enumstr)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), enumstr,true);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        /// <summary>
        /// 把枚举字符串转成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumstr"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int enumint)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), enumint.ToString());
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }



   
}

