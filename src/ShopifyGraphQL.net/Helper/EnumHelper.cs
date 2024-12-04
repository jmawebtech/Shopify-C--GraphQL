namespace ShopifyGraphQL.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// Takes the EnumMember value field and matches it.
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetString(Type enumType, string value)
        {
            foreach (string name in System.Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).FirstOrDefault();

                if (enumMemberAttribute == null)
                    continue;

                if (enumMemberAttribute.Value == value || name == value)
                    return enumMemberAttribute.Value;
            }

            return value;
        }
    }
}
