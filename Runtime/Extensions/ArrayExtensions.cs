namespace OmegaLeo.Toolbox.Runtime.Extensions
{
    public static class ArrayExtensions
    {
        // Idea obtained from TaroDev's video on things to do in Unity - https://youtu.be/Ic5ux-tpkCE?t=302
        public static T Next<T>(this T[] array, int currentIndex)
        {
            return array[currentIndex++ % array.Length];
        }
    }
}