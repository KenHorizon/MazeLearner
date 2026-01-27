using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace MazeLearner.Localization
{
    internal class LocalizationManager
    {
        public const string DEFAULT_CULTURE_CODE = "en-EN";

        public static List<CultureInfo> GetSupportedCultures()
        {
            // Create a list to hold supported cultures
            List<CultureInfo> supportedCultures = new List<CultureInfo>();

            // Get the current assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Resource manager for your Resources.resx
            ResourceManager resourceManager = new ResourceManager("MazeLearner.Localization.Resources", assembly);

            // Get all cultures defined in the satellite assemblies
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    // Try to get the resource set for this culture
                    var resourceSet = resourceManager.GetResourceSet(culture, true, false);
                    if (resourceSet != null)
                    {
                        supportedCultures.Add(culture);
                    }
                }
                catch (MissingManifestResourceException)
                {
                    // This exception is thrown when there is no .resx for the culture, ignore it
                }
            }

            // Always add the default (invariant) culture - the base .resx file
            supportedCultures.Add(CultureInfo.InvariantCulture);

            return supportedCultures;
        }

        /// <summary>
        /// Sets the current culture of the game based on the specified culture code.
        /// This method updates both the current culture and UI culture for the current thread.
        /// </summary>
        /// <param name="cultureCode">The culture code (e.g., "en-US", "fr-FR") to set for the game.</param>
        /// <remarks>
        /// This method modifies the <see cref="Thread.CurrentThread.CurrentCulture"/> and <see cref="Thread.CurrentThread.CurrentUICulture"/> properties,
        /// which affect how dates, numbers, and other culture-specific values are formatted, as well as how localized resources are loaded.
        /// </remarks>
        public static void SetCulture(string cultureCode)
        {
            if (string.IsNullOrEmpty(cultureCode))
                cultureCode = DEFAULT_CULTURE_CODE;

            // Create a CultureInfo object from the culture code
            CultureInfo culture = new CultureInfo(cultureCode);

            // Set the current culture and UI culture for the current thread
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}