namespace MVS.Scaffolding
{
    public static class StringExtension
    {
        public static string ReplaceLast(this string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if(place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
    }
}