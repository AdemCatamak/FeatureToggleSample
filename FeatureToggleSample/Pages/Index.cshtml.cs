using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace FeatureToggleSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFeatureManager _featureManager;

        public string ExternalSystemIntegrationStatus = "unknown";
        public DateTime Now;
        public bool IsPreviewActive;


        public IndexModel(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public async Task OnGet()
        {
            bool isExternalSystemActive = await _featureManager.IsEnabledAsync(FeatureFlags.EXTERNAL_SYSTEM_INTEGRATION);
            ExternalSystemIntegrationStatus = isExternalSystemActive ? "active" : "inactive";

            bool isUtcActive = await _featureManager.IsEnabledAsync(FeatureFlags.USE_UTC);
            if (isUtcActive)
                Now = DateTime.UtcNow;
            else
                Now = DateTime.Now;

            IsPreviewActive = await _featureManager.IsEnabledAsync(FeatureFlags.PREVIEW_ACTIVE);
        }
    }
}