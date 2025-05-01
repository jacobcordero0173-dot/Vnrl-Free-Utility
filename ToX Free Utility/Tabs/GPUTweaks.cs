using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToX_Free_Utility.LoadingForms;

namespace ToX_Free_Utility.Tabs
{
    public partial class GPUTweaks : Form
    {
        public GPUTweaks()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            GeneralNvidia.Checked = Properties.Settings.Default.GeneralNvidia;
            NvHidden.Checked = Properties.Settings.Default.NvHidden;
            NviPstate.Checked = Properties.Settings.Default.NviPstate;
            NIP.Checked = Properties.Settings.Default.NIP;
            GeneralAMD.Checked = Properties.Settings.Default.GeneralAMD;
            AMDHidden.Checked = Properties.Settings.Default.AMDHidden;
            AMDPStates.Checked = Properties.Settings.Default.AMDPStates;
            AmdSC.Checked = Properties.Settings.Default.AmdSC;
        }

        private void SaveState(Guna2ToggleSwitch Tswitch, string Tbool)
        {
            Properties.Settings.Default[Tbool] = Tswitch.Checked;
            Properties.Settings.Default.Save();
        }

        private void GeneralNvidia_CheckedChanged(object sender, EventArgs e) => SaveState(GeneralNvidia, "GeneralNvidia");
        private void NvHidden_CheckedChanged(object sender, EventArgs e) => SaveState(NvHidden, "NvHidden");
        private void NviPstate_CheckedChanged(object sender, EventArgs e) => SaveState(NviPstate, "NviPstate");
        private void NIP_CheckedChanged(object sender, EventArgs e) => SaveState(NIP, "NIP");
        private void GeneralAMD_CheckedChanged(object sender, EventArgs e) => SaveState(GeneralAMD, "GeneralAMD");
        private void AMDHidden_CheckedChanged(object sender, EventArgs e) => SaveState(AMDHidden, "AMDHidden");
        private void AMDPStates_CheckedChanged(object sender, EventArgs e) => SaveState(AMDPStates, "AMDPStates");
        private void AmdSC_CheckedChanged(object sender, EventArgs e) => SaveState(AmdSC, "AmdSC");

        private async void GeneralNvidia_Click(object sender, EventArgs e)
        {
            if (GeneralNvidia.Checked) await RunWithLoadingScreenAsync(NvidiaGeneralTweaks);
            else RevertNvidiaGeneralTweaks();
        }

        private async void NvHidden_Click(object sender, EventArgs e)
        {
            if (NvHidden.Checked) await RunWithLoadingScreenAsync(NvidiaHidden);
            else RevertNvidiaHidden();
        }

        private async void NviPstate_Click(object sender, EventArgs e)
        {
            if (NviPstate.Checked) await RunWithLoadingScreenAsync(NvidiaPStates);
            else RevertNvidiaPStates();
        }

        private async void NIP_Click(object sender, EventArgs e)
        {
            if (NIP.Checked) await RunWithLoadingScreenAsync(TweakedNip);
            else RevertNip();
        }

        private async void GeneralAMD_Click(object sender, EventArgs e)
        {
            if (GeneralAMD.Checked) await RunWithLoadingScreenAsync(AMDGeneralTweaks);
            else RevertAMDGeneralTweaks();
        }

        private async void AMDHidden_Click(object sender, EventArgs e)
        {
            if (AMDHidden.Checked) await RunWithLoadingScreenAsync(AmdHidden);
            else RevertAmdHidden();
        }

        private async void AMDPStates_Click(object sender, EventArgs e)
        {
            if (AMDPStates.Checked) await RunWithLoadingScreenAsync(AMDPstates);
            else RevertAMDPstates();
        }

        private async void AmdSC_Click(object sender, EventArgs e)
        {
            if (AmdSC.Checked) await RunWithLoadingScreenAsync(AMDShaderCache);
            else RevertAMDShaderCache();
        }

        private async Task RunWithLoadingScreenAsync(Action tweakAction)
        {
            var loadingForm = new TweaksLoading
            {
                StartPosition = FormStartPosition.CenterParent,
                TopMost = true
            };

            var mainForm = (Main)this.ParentForm;
            TweaksLoading.ShowModal(mainForm, loadingForm);

            await Task.Delay(2000);
            await Task.Run(tweakAction);

            loadingForm.CloseModal();

            var successForm = new TweaksSuccess
            {
                StartPosition = FormStartPosition.CenterParent,
                TopMost = true
            };
            await TweaksSuccess.ShowModal(mainForm, successForm);
        }
        private void AMDGeneralTweaks()
        {
            string batchCommands = @"for /f %i in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"" /s /v ""DriverDesc""^| findstr ""HKEY AMD ATI""') do if /i ""%i"" neq ""DriverDesc"" (set ""REGPATH_AMD=%i"")
for /f %i in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"" /s /v ""DriverDesc""^| findstr ""HKEY AMD ATI""') do if /i ""%i"" neq ""DriverDesc"" (set ""REGPATH_AMD=%i"")
reg add ""%REGPATH_AMD%"" /v ""3D_Refresh_Rate_Override_DEF"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""3to2Pulldown_NA"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""AAF_NA"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""Adaptive De-interlacing"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""AllowRSOverlay"" /t Reg_SZ /d ""false"" /f
reg add ""%REGPATH_AMD%"" /v ""AllowSkins"" /t Reg_SZ /d ""false"" /f
reg add ""%REGPATH_AMD%"" /v ""AllowSnapshot"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""AllowSubscription"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""AntiAlias_NA"" /t Reg_SZ /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""AreaAniso_NA"" /t Reg_SZ /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""ASTT_NA"" /t Reg_SZ /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""AutoColorDepthReduction_NA"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""DisableSAMUPowerGating"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""DisableUVDPowerGatingDynamic"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""DisableVCEPowerGating"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""EnableAspmL0s"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""EnableAspmL1"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""EnableUlps"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""EnableUlps_NA"" /t Reg_SZ /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""KMD_DeLagEnabled"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""KMD_FRTEnabled"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""DisableDMACopy"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""DisableBlockWrite"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""StutterMode"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""EnableUlps"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""PP_SclkDeepSleepDisable"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""PP_ThermalAutoThrottlingEnable"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%"" /v ""DisableDrmdmaPowerGating"" /t Reg_DWORD /d ""1"" /f
reg add ""%REGPATH_AMD%"" /v ""KMD_EnableComputePreemption"" /t Reg_DWORD /d ""0"" /f
reg add ""%REGPATH_AMD%\UMD"" /v ""Main3D_DEF"" /t Reg_SZ /d ""1"" /f
reg add ""%REGPATH_AMD%\UMD"" /v ""Main3D"" /t Reg_BINARY /d ""3100"" /f
reg add ""%REGPATH_AMD%\UMD"" /v ""FlipQueueSize"" /t Reg_BINARY /d ""3100"" /f
reg add ""%REGPATH_AMD%\UMD"" /v ""Tessellation_OPTION"" /t Reg_BINARY /d ""3200"" /f
reg add ""%REGPATH_AMD%\UMD"" /v ""Tessellation"" /t Reg_BINARY /d ""3100"" /f
reg add ""%REGPATH_AMD%\UMD"" /v ""VSyncControl"" /t Reg_BINARY /d ""3000"" /f
reg add ""%REGPATH_AMD%\UMD"" /v ""TFQ"" /t Reg_BINARY /d ""3200"" /f
reg add ""%REGPATH_AMD%\DAL2_DATA__2_0\DisplayPath_4\EDID_D109_78E9\Option"" /v ""ProtectionControl"" /t Reg_BINARY /d ""0100000001000000"" /f
";

            ExecuteBatchCommands(batchCommands);
        }

        private void AmdHidden()
        {
            string batchCommands = @"reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""3D_Refresh_Rate_Override_DEF"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowSnapshot"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AAF_NA"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AntiAlias_NA"" /t REG_SZ /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""ASTT_NA"" /t REG_SZ /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowSubscription"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AreaAniso_NA"" /t REG_SZ /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowRSOverlay"" /t REG_SZ /d ""false"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""Adaptive De-interlacing"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowSkins"" /t REG_SZ /d ""false"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AutoColorDepthReduction_NA"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableSAMUPowerGating"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableUVDPowerGatingDynamic"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableVCEPowerGating"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisablePowerGating"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDrmdmaPowerGating"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableVceSwClockGating"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUvdClockGating"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableAspmL0s"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableAspmL1"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUlps"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUlps_NA"" /t REG_SZ /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_DeLagEnabled"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_FRTEnabled"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDMACopy"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableBlockWrite"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""StutterMode"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PP_SclkDeepSleepDisable"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PP_ThermalAutoThrottlingEnable"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_EnableComputePreemption"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Main3D_DEF"" /t REG_SZ /d ""1"" /f
schtasks /change /disable /tn ""IntelSURQC-Upgrade-86621605-2a0b-4128-8ffc-15514c247132"" >nul 2>&1 
schtasks /change /disable /tn ""IntelSURQC-Upgrade-86621605-2a0b-4128-8ffc-15514c247132-Logon"" >nul 2>&1 
schtasks /change /disable /tn ""Intel PTT EK Recertification"" >nul 2>&1 
schtasks /change /disable /tn ""USER_ESRV_SVC_QUEENCREEK"" >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""GlobalTimerResolutionRequests"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Serialize"" /v ""StartupDelayInMSec"" /t REG_DWORD /d ""4096"" /f 
Reg add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer"" /v ""HubMode"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\PriorityControl"" /v ""Win32PrioritySeparation"" /t REG_DWORD /d ""38"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters"" /v ""EnablePrefetcher"" /t Reg_DWORD /d ""0"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters"" /v ""EnableSuperfetch"" /t Reg_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppPrivacy"" /v ""LetAppsRunInBackground"" /t Reg_DWORD /d ""2"" /f 
Reg add ""HKCU\Control Panel\Desktop"" /v ""WaitToKillAppTimeout"" /t Reg_SZ /d ""1000"" /f 
Reg add ""HKLM\System\CurrentControlSet\Control"" /v ""WaitToKillServiceTimeout"" /t Reg_SZ /d ""1000"" /f 
Reg add ""HKCU\Control Panel\Desktop"" /v ""HungAppTimeout"" /t Reg_SZ /d ""1000"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettings"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettingsOverride"" /t REG_DWORD /d ""3"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettingsOverrideMask"" /t REG_DWORD /d ""3"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting"" /v ""DontSendAdditionalData"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting"" /v ""LoggingDisabled"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultOverrideBehavior"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultConsent"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""ConnectedSearchUseWeb"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\CurrentVersion\Search"" /v ""BingSearchEnabled"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""DisableWebSearch"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile"" /v ""NetworkThrottlingIndex"" /t REG_DWORD /d ""10"" /f 
Reg add ""HKLM\Software\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile"" /v ""SystemResponsiveness"" /t REG_DWORD /d ""10"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""AITEnable"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""AllowTelemetry"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisableInventory"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisableUAR"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisableEngine"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisablePCA"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\Software\Microsoft\FTH"" /v ""Enable"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\StickyKeys"" /v ""Flags"" /t REG_SZ /d ""386"" /f 
@echo Sleep 
wevtutil sl Microsoft-Windows-SleepStudy/Diagnostic /e:false 
wevtutil sl Microsoft-Windows-Kernel-Processor-Power/Diagnostic /e:false 
wevtutil sl Microsoft-Windows-UserModePowerService/Diagnostic /e:false 
cls 
@echo Fsutil 
timeout /t 2 /nobreak >nul 
if exist ""%windir%\System32\fsutil.exe"" ( 
fsutil behavior set disablelastaccess 1 
fsutil behavior set disable8dot3 1 
) 
timeout /t 3 /nobreak >nul 
cls 
@echo AMD Tweaks 
Reg add ""HKCU\Software\AMD\CN"" /v ""AutoUpdateTriggered"" /t REG_DWORD /d ""0"" /f   
Reg add ""HKCU\Software\AMD\CN"" /v ""PowerSaverAutoEnable_CUR"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""WindowSize"" /t REG_SZ /d ""1440,960"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""BuildType"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""WizardProfile"" /t REG_SZ /d ""PROFILE_CUSTOM"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""UserTypeWizardShown"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""LastPage"" /t REG_SZ /d ""settings/graphics/0/"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""AutoUpdate"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""RSXBrowserUnavailable"" /t REG_SZ /d ""true"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""SystemTray"" /t REG_SZ /d ""false"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""AllowWebContent"" /t REG_SZ /d ""false"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""CN_Hide_Toast_Notification"" /t REG_SZ /d ""true"" /f  
Reg add ""HKCU\Software\AMD\CN"" /v ""AnimationEffect"" /t REG_SZ /d ""false"" /f  
Reg add ""HKCU\Software\AMD\CN\OverlayNotification"" /v ""AlreadyNotified"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\CN\VirtualSuperResolution"" /v ""AlreadyNotified"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""PerformanceMonitorOpacityWA"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""CaptureFileOutput"" /t REG_SZ /d ""C:\Users\Emre\Videos\Radeon ReLive"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""DvrEnabled"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""ActiveSceneId"" /t REG_SZ /d ""0"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""AVCCaps"" /t REG_SZ /d ""256,1,4096,4096,100000000,244800,0;"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""HEVCCaps"" /t REG_SZ /d ""256,1,4096,4096,2147483647,979200,0;"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""AvcEfcSupport"" /t REG_SZ /d ""0;"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""HevcEfcSupport"" /t REG_SZ /d ""0;"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""PrevInstantReplayEnable"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""PrevInGameReplayEnabled"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""PrevInstantGifEnabled"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""DvrDesktops"" /t REG_SZ /d ""\\.\DISPLAY19"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""RemoteServerStatus"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\DVR"" /v ""ShowRSOverlay"" /t REG_SZ /d ""false"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraSize"" /t REG_DWORD /d ""3"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraEnabled"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraOpacity"" /t REG_DWORD /d ""100"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraAnchor"" /t REG_DWORD /d ""3"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraShownOnScreen"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""IndicatorPosition"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""TimerEnabled"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""ChatOverlayEnabled"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""ChatCustomOffset"" /t REG_SZ /d ""0.0260,0.0462"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""ChatOverlayAnchor"" /t REG_DWORD /d ""4"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""ChatBackgroundBlur"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""ChatFontSize"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""RelativeCoords"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraOffset"" /t REG_SZ /d ""0.0208,0.0370"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraCustomOffset"" /t REG_SZ /d ""0.0208,0.0370"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraRect"" /t REG_SZ /d ""0.1667,0.2222"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""CameraCustomRect"" /t REG_SZ /d ""0.1667,0.2222"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""ChatCustomRect"" /t REG_SZ /d ""0.1562,0.1562"" /f  
Reg add ""HKCU\Software\AMD\SCENE\0"" /v ""ChatOverlaySize"" /t REG_DWORD /d ""3"" /f  
Reg add ""HKCU\Software\ATI\ACE\Settings\ADL\AppProfiles"" /v ""AplReloadCounter"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\Software\AMD\Install"" /v ""AUEP"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKLM\Software\AUEP"" /v ""RSX_AUEPStatus"" /t REG_DWORD /d ""2"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""NotifySubscription"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""IsComponentControl"" /t REG_BINARY /d ""00000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_USUEnable"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_RadeonBoostEnabled"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""IsAutoDefault"" /t REG_BINARY /d ""01000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_ChillEnabled"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_DeLagEnabled"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""ACE"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AnisoDegree_SET"" /t REG_BINARY /d ""3020322034203820313600"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Main3D_SET"" /t REG_BINARY /d ""302031203220332034203500"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Tessellation_OPTION"" /t REG_BINARY /d ""3200"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Tessellation"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AAF"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""GI"" /t REG_BINARY /d ""31000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""CatalystAI"" /t REG_BINARY /d ""31000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""TemporalAAMultiplier_NA"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ForceZBufferDepth"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""EnableTripleBuffering"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ExportCompressedTex"" /t REG_BINARY /d ""31000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""PixelCenter"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ZFormats_NA"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""DitherAlpha_NA"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""SwapEffect_D3D_SET"" /t REG_BINARY /d ""3020312032203320342038203900"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""TFQ"" /t REG_BINARY /d ""3200"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""VSyncControl"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""TextureOpt"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""TextureLod"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ASE"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ASD"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ASTT"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AntiAliasSamples"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AntiAlias"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AnisoDegree"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AnisoType"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AntiAliasMapping_SET"" /t REG_BINARY /d ""3028303A302C313A3029203228303A322C313A3229203428303A342C313A3429203828303A382C313A382900"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AntiAliasSamples_SET"" /t REG_BINARY /d ""3020322034203800"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ForceZBufferDepth_SET"" /t REG_BINARY /d ""3020313620323400"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""SwapEffect_OGL_SET"" /t REG_BINARY /d ""3020312032203320342035203620372038203920313120313220313320313420313520313620313700"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Tessellation_SET"" /t REG_BINARY /d ""31203220342036203820313620333220363400"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""HighQualityAF"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""DisplayCrossfireLogo"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AppGpuId"" /t REG_BINARY /d ""300078003000310030003000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""SwapEffect"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""PowerState"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""AntiStuttering"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""TurboSync"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""SurfaceFormatReplacements"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""EQAA"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ShaderCache"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""MLF"" /t REG_BINARY /d ""3000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""TruformMode_NA"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""LRTCEnable"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""3to2Pulldown"" /t REG_BINARY /d ""31000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""MosquitoNoiseRemoval_ENABLE"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""MosquitoNoiseRemoval"" /t REG_BINARY /d ""350030000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Deblocking_ENABLE"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Deblocking"" /t REG_BINARY /d ""350030000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""DemoMode"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""OverridePA"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""DynamicRange"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""StaticGamma_ENABLE"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""BlueStretch_ENABLE"" /t REG_BINARY /d ""31000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""BlueStretch"" /t REG_BINARY /d ""31000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""LRTCCoef"" /t REG_BINARY /d ""3100300030000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""DynamicContrast_ENABLE"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""WhiteBalanceCorrection"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Fleshtone_ENABLE"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Fleshtone"" /t REG_BINARY /d ""350030000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""ColorVibrance_ENABLE"" /t REG_BINARY /d ""31000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""ColorVibrance"" /t REG_BINARY /d ""340030000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Detail_ENABLE"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Detail"" /t REG_BINARY /d ""310030000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Denoise_ENABLE"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""Denoise"" /t REG_BINARY /d ""360034000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""TrueWhite"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""OvlTheaterMode"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""StaticGamma"" /t REG_BINARY /d ""3100300030000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD\DXVA"" /v ""InternetVideo"" /t REG_BINARY /d ""30000000"" /f  
Reg add ""HKLM\System\CurrentControlSet\Services\amdwddmg"" /v ""ChillEnabled"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Main3D_DEF"" /t REG_SZ /d ""1"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Main3D"" /t REG_BINARY /d ""3100"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDMACopy"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableBlockWrite"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PP_ThermalAutoThrottlingEnable"" /t REG_DWORD /d ""0"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDrmdmaPowerGating"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKLM\System\CurrentControlSet\Services\AMD Crash Defender Service"" /v ""Start"" /t REG_DWORD /d ""4"" /f  
Reg add ""HKLM\System\CurrentControlSet\Services\AMD External Events Utility"" /v ""Start"" /t REG_DWORD /d ""4"" /f  
Reg add ""HKLM\System\CurrentControlSet\Services\amdfendr"" /v ""Start"" /t REG_DWORD /d ""4"" /f  
Reg add ""HKLM\System\CurrentControlSet\Services\amdfendrmgr"" /v ""Start"" /t REG_DWORD /d ""4"" /f  
Reg add ""HKLM\System\CurrentControlSet\Services\amdlog"" /v ""Start"" /t REG_DWORD /d ""4"" /f  
sc config amdlog start=disabled 
sc config ""AMD External Events Utility"" start=disabled 
timeout /t 3 /nobreak >nul 
cls		 
@echo USB Power 
for %%i in (EnhancedPowerManagementEnabled AllowIdleIrpInD3 EnableSelectiveSuspend DeviceSelectiveSuspended 
        SelectiveSuspendEnabled SelectiveSuspendOn EnumerationRetryCount ExtPropDescSemaphore WaitWakeEnabled 
        D3ColdSupported WdfDirectedPowerTransitionEnable EnableIdlePowerManagement IdleInWorkingState) do for /f %%a in ('Reg query ""HKLM\SYSTEM\CurrentControlSet\Enum"" /s /f ""%%i""^| findstr ""HKEY""') do Reg add ""%%a"" /v ""%%i"" /t REG_DWORD /d ""0"" /f >nul 2>&1 
for /f %%i in ('wmic path Win32_IDEController get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_USBController get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_VideoController get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_NetworkAdapter get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_SoundDevice get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""0"" /f  
for /f ""tokens=*"" %%i in ('Reg query ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\PCI""^| findstr ""HKEY""') do ( 
			for /f ""tokens=*"" %%a in ('Reg query ""%%i""^| findstr ""HKEY""') do Reg delete ""%%a\Device Parameters\Interrupt Management\Affinity Policy"" /v ""DevicePriority"" /f >nul 2>&1 
		) 
) 
cls 
@echo MSI 
timeout /t 2 /nobreak >nul 
for /f %%s in ('wmic PATH Win32_PnPEntity GET DeviceID ^| findstr /l ""USB\VID_""') do ( 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" -ot Reg -actn setowner -ownr ""n:Administrators"" >nul 2>&1 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" -ot Reg -actn ace -ace ""n:Administrators;p:full"" >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v SelectiveSuspendOn /t REG_DWORD /d 00000000 /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v SelectiveSuspendEnabled /t REG_BINARY /d 00 /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v EnhancedPowerManagementEnabled /t REG_DWORD /d 00000000 /f >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v AllowIdleIrpInD3 /t REG_DWORD /d 00000000 /f >nul 2>&1 
	) 
for /f %%s in ('wmic PATH Win32_USBHub GET DeviceID ^| findstr /l ""USB\ROOT_HUB""') do ( 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters\WDF"" -ot Reg -actn setowner -ownr ""n:Administrators"" >nul 2>&1 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" -ot Reg -actn ace -ace ""n:Administrators;p:full"" >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters\WDF"" /v IdleInWorkingState /t REG_DWORD /d 00000000 /f >nul 2>&1 
	) 
for /f ""tokens=*"" %%s in ('reg query ""HKLM\System\CurrentControlSet\Enum"" /S /F ""StorPort"" ^| findstr /e ""StorPort""') do Reg add ""%%s"" /v ""EnableIdlePowerManagement"" /t REG_DWORD /d ""0"" /f 
cls		   
@echo Delay 
timeout /t 2 /nobreak >nul 
Reg add ""HKCU\Control Panel\Desktop"" /v ""MenuShowDelay"" /t REG_SZ /d ""0"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseSensitivity"" /t REG_SZ /d ""10"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseSpeed"" /t REG_SZ /d ""0"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseThreshold1"" /t REG_SZ /d ""0"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseThreshold2"" /t REG_SZ /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseSpeed"" /t REG_SZ /d ""0"" /f  
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseThreshold1"" /t REG_SZ /d ""0"" /f  
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseThreshold2"" /t REG_SZ /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""SmoothMouseXCurve"" /t REG_BINARY /d 0000000000000000C0CC0C0000000000809919000000000040662600000000000033330000000000 /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""SmoothMouseYCurve"" /t REG_BINARY /d 0000000000000000000038000000000000007000000000000000A800000000000000E00000000000 /f 
Reg add ""HKCU\Control Panel\Mouse"" /v ""SmoothMouseXCurve"" /t REG_BINARY /d 0000000000000000C0CC0C0000000000809919000000000040662600000000000033330000000000 /f 
Reg add ""HKCU\Control Panel\Mouse"" /v ""SmoothMouseYCurve"" /t REG_BINARY /d 0000000000000000000038000000000000007000000000000000A800000000000000E00000000000 /f 
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseHoverTime"" /t REG_SZ /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseHoverTime"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Mouse"" /v ""ActiveWindowTracking"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""ActiveWindowTracking"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Preference"" /v ""On"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""AutoRepeatDelay"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""AutoRepeatRate"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""BounceTime"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""DelayBeforeAcceptance"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last BounceKey Setting"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last Valid Delay"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last Valid Repeat"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last Valid Wait"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""MaximumSpeed"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""TimeToMaximumSpeed"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Keyboard"" /v ""InitialKeyboardIndicators"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Keyboard"" /v ""KeyboardDelay"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Keyboard"" /v ""KeyboardSpeed"" /t REG_SZ /d ""31"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Keyboard"" /v ""KeyboardDelay"" /t REG_SZ /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Keyboard"" /v ""KeyboardSpeed"" /t REG_SZ /d ""31"" /f 
Reg add ""HKLM\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorSpeed"" /v ""CursorSensitivity"" /t REG_DWORD /d ""10000"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\StickyKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\ToggleKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 

@echo off
for /L %%i in (0,1,9) do (
    for /F ""tokens=2* skip=2"" %%a in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000%%i"" /v ""ProviderName"" 2^>nul') do (
	if /i ""%%b""==""Advanced Micro Devices, Inc."" (
		set G=000%%i
		)
	)
)
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""EnableUlps"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_ThermalAutoThrottlingEnable"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_SclkDeepSleepDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""DisableDMACopy"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""DisableBlockWrite"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""DisableDrmdmaPowerGating"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_GPUPowerDownEnabled"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""DisableVCEPowerGating"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""DisableUVDPowerGatingDynamic"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""DisableSAMUPowerGating"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""GCOOPTION_DisableGPIOPowerSaveMode"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""EnableAspmL0s"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""EnableAspmL1"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""EnableUvdClockGating"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""EnableVceSwClockGating"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_DisablePowerContainment"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_AllGraphicLevelsEnabled"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_DynamicClockEnable"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_ActivityTarget"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PowerTuneEnable"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_MCLKDeepSleepDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_ForcePowerDownEnable"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_DCEFCLKDeepSleepDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_DPMFStatesDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_DALClockGatingDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PcieAspm"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_ClkStutterModeDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_StateTransitionDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_GfxClockGatingDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""EnableUlps_NB"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_SclkDeepSleepEnable"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_MemoryClockGatingDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PP_GpuClockGatingDisable"" /t REG_DWORD /d ""1"" /f > nul 2>&1
Reg.exe add ""HKCU\Software\AMD\CN"" /v ""AutoUpdateTriggered"" /t REG_DWORD /d ""0"" /f > nul 2>&1 > nul 2>&1
Reg.exe add ""HKCU\Software\AMD\CN"" /v ""AutoUpdate"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKCU\Software\AMD\CN"" /v ""AnimationEffect"" /t REG_SZ /d ""false"" /f > nul 2>&1
Reg.exe add ""HKCU\Software\AMD\CN"" /v ""PowerSaverAutoEnable_CUR"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKCU\Software\AMD\CN"" /v ""AllowWebContent"" /t REG_SZ /d ""false"" /f > nul 2>&1
Reg.exe add ""HKCU\Software\AMD\CN"" /v ""CN_Hide_Toast_Notification"" /t REG_SZ /d ""true"" /f > nul 2>&1
Reg.exe add ""HKCU\Software\AMD\CN"" /v ""AnimationEffect"" /t REG_SZ /d ""false"" /f > nul 2>&1

@echo off
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDMACopy"" /t REG_DWORD /d 1 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableBlockWrite"" /t REG_DWORD /d 0 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""StutterMode"" /t REG_DWORD /d 0 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUlps"" /t REG_DWORD /d 0 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUlps_NA"" /t REG_DWORD /d 0 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PP_SclkDeepSleepDisable"" /t REG_DWORD /d 1 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PP_ThermalAutoThrottlingEnable"" /t REG_DWORD /d 0 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDrmdmaPowerGating"" /t REG_DWORD /d 1 /f

Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Main3D_DEF"" /t REG_SZ /d ""1"" /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Main3D"" /t REG_BINARY /d 3100 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""FlipQueueSize"" /t REG_BINARY /d 3100 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ShaderCache"" /t REG_BINARY /d 3200 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Tessellation_OPTION"" /t REG_BINARY /d 3200 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Tessellation"" /t REG_BINARY /d 3100 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""VSyncControl"" /t REG_BINARY /d 3000 /f
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""TFQ"" /t REG_BINARY /d 3200 /f

Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\DAL2_DATA__2_0\DisplayPath_4\EDID_D109_78E9\Option"" /v ""ProtectionControl"" /t REG_BINARY /d 0100000001000000 /f

echo Registry changes applied successfully.

Reg.exe add ""HKLM\System\CurrentControlSet\Services\amdwddmg"" /v ""ChillEnabled"" /t REG_DWORD /d ""0"" /f > nul 2>&1
Reg.exe add ""HKLM\System\CurrentControlSet\Services\AMD Crash Defender Service"" /v ""Start"" /t REG_DWORD /d ""4"" /f > nul 2>&1
Reg.exe add ""HKLM\System\CurrentControlSet\Services\AMD External Events Utility"" /v ""Start"" /t REG_DWORD /d ""4"" /f > nul 2>&1
Reg.exe add ""HKLM\System\CurrentControlSet\Services\amdfendr"" /v ""Start"" /t REG_DWORD /d ""4"" /f > nul 2>&1
Reg.exe add ""HKLM\System\CurrentControlSet\Services\amdfendrmgr"" /v ""Start"" /t REG_DWORD /d ""4"" /f > nul 2>&1
Reg.exe add ""HKLM\System\CurrentControlSet\Services\amdlog"" /v ""Start"" /t REG_DWORD /d ""4"" /f > nul 2>&1
sc config amdlog start=disabled
sc config ""AMD External Events Utility"" start=disabled
cls
echo Powersavings and services successfully disabled.
";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertAmdHidden()
        {
            string batchCommands = @"reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""3D_Refresh_Rate_Override_DEF"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowSnapshot"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AAF_NA"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AntiAlias_NA"" /t REG_SZ /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""ASTT_NA"" /t REG_SZ /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowSubscription"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AreaAniso_NA"" /t REG_SZ /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowRSOverlay"" /t REG_SZ /d ""false"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""Adaptive De-interlacing"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AllowSkins"" /t REG_SZ /d ""false"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""AutoColorDepthReduction_NA"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableSAMUPowerGating"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableUVDPowerGatingDynamic"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableVCEPowerGating"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisablePowerGating"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDrmdmaPowerGating"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableVceSwClockGating"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUvdClockGating"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableAspmL0s"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableAspmL1"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUlps"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""EnableUlps_NA"" /t REG_SZ /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_DeLagEnabled"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_FRTEnabled"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDMACopy"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableBlockWrite"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""StutterMode"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PP_SclkDeepSleepDisable"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PP_ThermalAutoThrottlingEnable"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""KMD_EnableComputePreemption"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""Main3D_DEF"" /t REG_SZ /d ""1"" /f
";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertAMDGeneralTweaks()
        {
            string batchCommands = @"for /f %i in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"" /s /v ""DriverDesc""^| findstr ""HKEY AMD ATI""') do if /i ""%i"" neq ""DriverDesc"" (set ""REGPATH_AMD=%i"")
for /f %i in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"" /s /v ""DriverDesc""^| findstr ""HKEY AMD ATI""') do if /i ""%i"" neq ""DriverDesc"" (set ""REGPATH_AMD=%i"")
reg delete ""%REGPATH_AMD%"" /v ""3D_Refresh_Rate_Override_DEF"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""3to2Pulldown_NA"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""AAF_NA"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""Adaptive De-interlacing"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""AllowRSOverlay"" /t Reg_SZ /d ""false"" /f
reg delete ""%REGPATH_AMD%"" /v ""AllowSkins"" /t Reg_SZ /d ""false"" /f
reg delete ""%REGPATH_AMD%"" /v ""AllowSnapshot"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""AllowSubscription"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""AntiAlias_NA"" /t Reg_SZ /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""AreaAniso_NA"" /t Reg_SZ /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""ASTT_NA"" /t Reg_SZ /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""AutoColorDepthReduction_NA"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""DisableSAMUPowerGating"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""DisableUVDPowerGatingDynamic"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""DisableVCEPowerGating"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""EnableAspmL0s"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""EnableAspmL1"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""EnableUlps"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""EnableUlps_NA"" /t Reg_SZ /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""KMD_DeLagEnabled"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""KMD_FRTEnabled"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""DisableDMACopy"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""DisableBlockWrite"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""StutterMode"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""EnableUlps"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""PP_SclkDeepSleepDisable"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""PP_ThermalAutoThrottlingEnable"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%"" /v ""DisableDrmdmaPowerGating"" /t Reg_DWORD /d ""1"" /f
reg delete ""%REGPATH_AMD%"" /v ""KMD_EnableComputePreemption"" /t Reg_DWORD /d ""0"" /f
reg delete ""%REGPATH_AMD%\UMD"" /v ""Main3D_DEF"" /t Reg_SZ /d ""1"" /f
reg delete ""%REGPATH_AMD%\UMD"" /v ""Main3D"" /t Reg_BINARY /d ""3100"" /f
reg delete ""%REGPATH_AMD%\UMD"" /v ""FlipQueueSize"" /t Reg_BINARY /d ""3100"" /f
reg delete ""%REGPATH_AMD%\UMD"" /v ""Tessellation_OPTION"" /t Reg_BINARY /d ""3200"" /f
reg delete ""%REGPATH_AMD%\UMD"" /v ""Tessellation"" /t Reg_BINARY /d ""3100"" /f
reg delete ""%REGPATH_AMD%\UMD"" /v ""VSyncControl"" /t Reg_BINARY /d ""3000"" /f
reg delete ""%REGPATH_AMD%\UMD"" /v ""TFQ"" /t Reg_BINARY /d ""3200"" /f
reg delete ""%REGPATH_AMD%\DAL2_DATA__2_0\DisplayPath_4\EDID_D109_78E9\Option"" /v ""ProtectionControl"" /t Reg_BINARY /d ""0100000001000000"" /f
";

            ExecuteBatchCommands(batchCommands);
        }
        private void AMDPstates()
        {
            string batchCommands = @"reg add ""HKLM\SOFTWARE\AMD\Energy\PowerState"" /v ""PStateControl"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SOFTWARE\AMD\Energy\PowerState"" /v ""PStateMode"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SOFTWARE\AMD\Energy\PowerState"" /v ""EnableDynamicPower"" /t REG_DWORD /d ""0"" /f
";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertAMDPstates()
        {
            string batchCommands = @"reg add ""HKLM\SOFTWARE\AMD\Energy\PowerState"" /v ""PStateControl"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SOFTWARE\AMD\Energy\PowerState"" /v ""PStateMode"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SOFTWARE\AMD\Energy\PowerState"" /v ""EnableDynamicPower"" /t REG_DWORD /d ""0"" /f
";

            ExecuteBatchCommands(batchCommands);
        }
        private void AMDShaderCache()
        {
            string batchCommands = @"reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ShaderCache"" /t REG_BINARY /d ""3200"" /f";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertAMDShaderCache()
        {
            string batchCommands = @"reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000\UMD"" /v ""ShaderCache"" /t REG_BINARY /d ""3200"" /f";

            ExecuteBatchCommands(batchCommands);
        }
        private void NvidiaGeneralTweaks()
        {
            string batchCommands = @"Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""TCCSupported"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKCU\SOFTWARE\NVIDIA Corporation\Global\NVTweak\Devices\509901423-0\Color"" /v ""NvCplUseColorCorrection"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v EnableRID73779  /t REG_DWORD /d 1 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v EnableRID73780  /t REG_DWORD /d 1 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v EnableRID74361  /t REG_DWORD /d 1 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v EnableRID44231  /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v EnableRID64640  /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v EnableRID66610  /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client"" /v OptInOrOutPreference /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\services\NvTelemetryContainer"" /v Start /t REG_DWORD /d 4 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\Startup"" /v SendTelemetryData /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\Startup\SendTelemetryData"" /v 0 /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""EnableMidBufferPreemption"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""RMDisablePostL2Compression"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""RmDisableRegistryCaching"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Throttle"" /v ""PerfEnablePackageIdle"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""0x112493bd"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""0x11e91a61"" /t REG_DWORD /d ""4294967295"" /f
Reg.exe add ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""DisableCudaContextPreemption"" /t REG_DWORD /d ""1"" /f
Reg.exe Add ""HKLM\SYSTEM\CurrentControlSet\Services\NvTelemetryContainer"" /v ""Start"" /t REG_DWORD /d ""4"" /f
Reg.exe Add ""HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client"" /v ""OptInOrOutPreference"" /t REG_DWORD /d ""0"" /f
Reg.exe Add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v ""EnableRID44231"" /t REG_DWORD /d ""0"" /f
Reg.exe Add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v ""EnableRID64640"" /t REG_DWORD /d ""0"" /f
Reg.exe Add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v ""EnableRID66610"" /t REG_DWORD /d ""0"" /f
Reg.exe Add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v ""EnableRID44231"" /t REG_DWORD /d ""0"" /f
Reg.exe Add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v ""EnableRID64640"" /t REG_DWORD /d ""0"" /f
Reg.exe Add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v ""EnableRID66610"" /t REG_DWORD /d ""0"" /f
Reg.exe Delete ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"" /v ""NvBackend"" /f
schtasks /change /disable /tn ""IntelSURQC-Upgrade-86621605-2a0b-4128-8ffc-15514c247132"" >nul 2>&1 
schtasks /change /disable /tn ""IntelSURQC-Upgrade-86621605-2a0b-4128-8ffc-15514c247132-Logon"" >nul 2>&1 
schtasks /change /disable /tn ""Intel PTT EK Recertification"" >nul 2>&1 
schtasks /change /disable /tn ""USER_ESRV_SVC_QUEENCREEK"" >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""GlobalTimerResolutionRequests"" /t REG_DWORD /d ""1"" /f  
Reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Serialize"" /v ""StartupDelayInMSec"" /t REG_DWORD /d ""4096"" /f 
Reg add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer"" /v ""HubMode"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\PriorityControl"" /v ""Win32PrioritySeparation"" /t REG_DWORD /d ""42"" /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters"" /v ""EnablePrefetcher"" /t Reg_DWORD /d ""0"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters"" /v ""EnableSuperfetch"" /t Reg_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppPrivacy"" /v ""LetAppsRunInBackground"" /t Reg_DWORD /d ""2"" /f 
Reg add ""HKCU\Control Panel\Desktop"" /v ""WaitToKillAppTimeout"" /t Reg_SZ /d ""1000"" /f 
Reg add ""HKLM\System\CurrentControlSet\Control"" /v ""WaitToKillServiceTimeout"" /t Reg_SZ /d ""1000"" /f 
Reg add ""HKCU\Control Panel\Desktop"" /v ""HungAppTimeout"" /t Reg_SZ /d ""1000"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettings"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettingsOverride"" /t REG_DWORD /d ""3"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettingsOverrideMask"" /t REG_DWORD /d ""3"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting"" /v ""DontSendAdditionalData"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting"" /v ""LoggingDisabled"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultOverrideBehavior"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultConsent"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""ConnectedSearchUseWeb"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Microsoft\Windows\CurrentVersion\Search"" /v ""BingSearchEnabled"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""DisableWebSearch"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile"" /v ""NetworkThrottlingIndex"" /t REG_DWORD /d ""10"" /f 
Reg add ""HKLM\Software\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile"" /v ""SystemResponsiveness"" /t REG_DWORD /d ""10"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""AITEnable"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""AllowTelemetry"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisableInventory"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisableUAR"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisableEngine"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\Software\Policies\Microsoft\Windows\AppCompat"" /v ""DisablePCA"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\Software\Microsoft\FTH"" /v ""Enable"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\StickyKeys"" /v ""Flags"" /t REG_SZ /d ""386"" /f 
@echo Sleep 
wevtutil sl Microsoft-Windows-SleepStudy/Diagnostic /e:false 
wevtutil sl Microsoft-Windows-Kernel-Processor-Power/Diagnostic /e:false 
wevtutil sl Microsoft-Windows-UserModePowerService/Diagnostic /e:false 
cls 
@echo Fsutil 
timeout /t 2 /nobreak >nul 
if exist ""%windir%\System32\fsutil.exe"" ( 
fsutil behavior set disablelastaccess 1 
fsutil behavior set disable8dot3 1 
) 
timeout /t 3 /nobreak >nul 
cls 
@echo Nvidia 
Reg add ""HKLM\SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DisableDynamicPstate"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmProfilingAdminOnly"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMHdcpKeyglobZero"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMHdcpKeyglobZero"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""DisableAsyncPstates"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\NVTweak"" /v ""NvDevToolsVisible"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\NVTweak\Features"" /v ""EnableFeature1"" /t REG_DWORD /d ""1"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\Startup"" /v ""SendTelemetryData"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Services\GpuEnergyDrv"" /v ""Start"" /t Reg_DWORD /d ""4"" /f 
Reg add ""HKCU\Software\NVIDIA Corporation\Global\NVTweak\Devices\509901423-0\Color"" /v ""NvCplUseColorCorrection"" /t Reg_DWORD /d ""0"" /f 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\NVTweak"" /v ""DisplayPowerSaving"" /t Reg_DWORD /d ""0"" /f 
if not exist ""%SystemDrive%\Program Files\NVIDIA Corporation\NVSMI"" mkdir ""%SystemDrive%\Program Files\NVIDIA Corporation\NVSMI"" 
copy /Y ""%windir%\system32\nvml.dll"" ""%SystemDrive%\Program Files\NVIDIA Corporation\NVSMI\nvml.dll"" 
cd ""C:\Program Files\NVIDIA Corporation\NVSMI\"" 
nvidia-smi -acp UNRESTRICTED 
Reg add ""HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client"" /v ""OptInOrOutPreference"" /t REG_DWORD /d ""0"" /f  
Reg delete ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"" /v ""NvBackend"" /f >nul 2>&1 
schtasks /Change /Disable /TN ""NvTmRep_CrashReport1_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvTmRep_CrashReport2_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvTmRep_CrashReport3_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvTmRep_CrashReport4_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvTmMon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvTmRep_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvNodeLauncher_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvProfileUpdaterOnLogon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvTmRepOnLogon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvProfileUpdaterDaily_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8"" 
schtasks /Change /Disable /TN ""NvDriverUpdateCheckDaily_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NVIDIA GeForce Experience SelfUpdate_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
schtasks /Change /Disable /TN ""NvTmMon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}"" 
timeout /t 5 /nobreak >nul 
cls 
@echo USB Power 
for %%i in (EnhancedPowerManagementEnabled AllowIdleIrpInD3 EnableSelectiveSuspend DeviceSelectiveSuspended 
        SelectiveSuspendEnabled SelectiveSuspendOn EnumerationRetryCount ExtPropDescSemaphore WaitWakeEnabled 
        D3ColdSupported WdfDirectedPowerTransitionEnable EnableIdlePowerManagement IdleInWorkingState) do for /f %%a in ('Reg query ""HKLM\SYSTEM\CurrentControlSet\Enum"" /s /f ""%%i""^| findstr ""HKEY""') do Reg add ""%%a"" /v ""%%i"" /t REG_DWORD /d ""0"" /f >nul 2>&1 
for /f %%i in ('wmic path Win32_IDEController get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_USBController get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_VideoController get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_NetworkAdapter get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""1"" /f  
for /f %%i in ('wmic path Win32_SoundDevice get PNPDeviceID^| findstr /l ""PCI\VEN_""') do Reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\%%i\Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"" /v ""MSISupported"" /t REG_DWORD /d ""0"" /f  
for /f ""tokens=*"" %%i in ('Reg query ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\PCI""^| findstr ""HKEY""') do ( 
			for /f ""tokens=*"" %%a in ('Reg query ""%%i""^| findstr ""HKEY""') do Reg delete ""%%a\Device Parameters\Interrupt Management\Affinity Policy"" /v ""DevicePriority"" /f >nul 2>&1 
		) 
) 
cls 
@echo MSI 
timeout /t 2 /nobreak >nul 
for /f %%s in ('wmic PATH Win32_PnPEntity GET DeviceID ^| findstr /l ""USB\VID_""') do ( 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" -ot Reg -actn setowner -ownr ""n:Administrators"" >nul 2>&1 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" -ot Reg -actn ace -ace ""n:Administrators;p:full"" >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v SelectiveSuspendOn /t REG_DWORD /d 00000000 /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v SelectiveSuspendEnabled /t REG_BINARY /d 00 /f  
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v EnhancedPowerManagementEnabled /t REG_DWORD /d 00000000 /f >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" /v AllowIdleIrpInD3 /t REG_DWORD /d 00000000 /f >nul 2>&1 
	) 
for /f %%s in ('wmic PATH Win32_USBHub GET DeviceID ^| findstr /l ""USB\ROOT_HUB""') do ( 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters\WDF"" -ot Reg -actn setowner -ownr ""n:Administrators"" >nul 2>&1 
SetACL.exe -on ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters"" -ot Reg -actn ace -ace ""n:Administrators;p:full"" >nul 2>&1 
Reg add ""HKLM\SYSTEM\CurrentControlSet\Enum\%%s\Device Parameters\WDF"" /v IdleInWorkingState /t REG_DWORD /d 00000000 /f >nul 2>&1 
	) 
for /f ""tokens=*"" %%s in ('reg query ""HKLM\System\CurrentControlSet\Enum"" /S /F ""StorPort"" ^| findstr /e ""StorPort""') do Reg add ""%%s"" /v ""EnableIdlePowerManagement"" /t REG_DWORD /d ""0"" /f 
cls 
@echo Delay 
timeout /t 2 /nobreak >nul 
Reg add ""HKCU\Control Panel\Desktop"" /v ""MenuShowDelay"" /t REG_SZ /d ""0"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseSensitivity"" /t REG_SZ /d ""10"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseSpeed"" /t REG_SZ /d ""0"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseThreshold1"" /t REG_SZ /d ""0"" /f  
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseThreshold2"" /t REG_SZ /d ""0"" /f  
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseSpeed"" /t REG_SZ /d ""0"" /f  
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseThreshold1"" /t REG_SZ /d ""0"" /f  
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseThreshold2"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Mouse"" /v ""SmoothMouseXCurve"" /t REG_BINARY /d 0000000000000000C0CC0C0000000000809919000000000040662600000000000033330000000000 /f 
Reg add ""HKCU\Control Panel\Mouse"" /v ""SmoothMouseYCurve"" /t REG_BINARY /d 0000000000000000000038000000000000007000000000000000A800000000000000E00000000000 /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""SmoothMouseXCurve"" /t REG_BINARY /d 0000000000000000C0CC0C0000000000809919000000000040662600000000000033330000000000 /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""SmoothMouseYCurve"" /t REG_BINARY /d 0000000000000000000038000000000000007000000000000000A800000000000000E00000000000 /f 
Reg add ""HKCU\Control Panel\Mouse"" /v ""MouseHoverTime"" /t REG_SZ /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""MouseHoverTime"" /t REG_SZ /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""ActiveWindowTracking"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Mouse"" /v ""ActiveWindowTracking"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Preference"" /v ""On"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""AutoRepeatDelay"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""AutoRepeatRate"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""BounceTime"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""DelayBeforeAcceptance"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last BounceKey Setting"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last Valid Delay"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last Valid Repeat"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Last Valid Wait"" /t REG_DWORD /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""MaximumSpeed"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""TimeToMaximumSpeed"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Keyboard"" /v ""InitialKeyboardIndicators"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Keyboard"" /v ""KeyboardDelay"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Keyboard"" /v ""KeyboardSpeed"" /t REG_SZ /d ""31"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Keyboard"" /v ""KeyboardDelay"" /t REG_SZ /d ""0"" /f 
Reg add ""HKU\.DEFAULT\Control Panel\Keyboard"" /v ""KeyboardSpeed"" /t REG_SZ /d ""31"" /f 
Reg add ""HKLM\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorSpeed"" /v ""CursorSensitivity"" /t REG_DWORD /d ""10000"" /f 
Reg add ""HKCU\Control Panel\Accessibility\MouseKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\StickyKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\Keyboard Response"" /v ""Flags"" /t REG_SZ /d ""0"" /f 
Reg add ""HKCU\Control Panel\Accessibility\ToggleKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f 

:: Batch script to apply registry tweaks
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v EnableRID61684 /t REG_DWORD /d 1 /f

:: Disable Preemption
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v DisablePreemption /t REG_DWORD /d 1 /f
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v DisableCudaContextPreemption /t REG_DWORD /d 1 /f

:: Disable Write Combining
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v DisableWriteCombining /t REG_DWORD /d 1 /f

:: Disable Preemption in Graphics Drivers
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v EnablePreemption /t REG_DWORD /d 0 /f

:: PowerMizer and Performance settings
for %%i in (0000 0001 0002 0003) do (
    reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%%i"" /v PerfLevelSrc /t REG_DWORD /d 0x8ae /f
    reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%%i"" /v PowerMizerEnable /t REG_DWORD /d 0 /f
    reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%%i"" /v PowerMizerLevel /t REG_DWORD /d 0 /f
    reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%%i"" /v PowerMizerLevelAC /t REG_DWORD /d 0 /f
)
";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertNvidiaGeneralTweaks()
        {
            string batchCommands = @"Reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v ""EnableRID61684"" /t REG_DWORD /d ""1"" /f

Reg delete ""HKCU\Software\NVIDIA Corporation\Global\NVTweak\Devices\509901423-0\Color"" /v ""NvCplUseColorCorrection"" /t REG_DWORD /d ""0"" /f
Reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d ""0"" /f
Reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\NVTweak"" /v ""DisplayPowerSaving"" /t REG_DWORD /d ""0"" /f

reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""D3PCLatency"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""F1TransitionLatency"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""LOWLATENCY"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""Node3DLowLatency"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PciLatencyTimerControl"" /t REG_DWORD /d ""20"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMDeepL1EntryLatencyUsec"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmGspcMaxFtuS"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmGspcMinFtuS"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmGspcPerioduS"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMLpwrEiIdleThresholdUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMLpwrGrIdleThresholdUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMLpwrGrRgIdleThresholdUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMLpwrMsIdleThresholdUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""VRDirectFlipDPCDelayUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""VRDirectFlipTimingMarginUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""VRDirectJITFlipMsHybridFlipDelayUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""vrrCursorMarginUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""vrrDeflickerMarginUs"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""vrrDeflickerMaxUs"" /t REG_DWORD /d ""1"" /f

reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMHdcpKeyGlobZero"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""TCCSupported"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""PreferSystemMemoryContiguous"" /t REG_DWORD /d ""1"" /f

reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\NVAPI"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\NVTweak"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f

reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmCacheLoc"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrLevel"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrDelay"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrDdiDelay"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrDebugMode"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrLimitCount"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrLimitTime"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""TdrTestMode"" /t REG_DWORD /d ""0"" /f

reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmFbsrPagedDMA"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""Acceleration.Level"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""DisablePreemption"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""DisableCudaContextPreemption"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""EnableCEPreemption"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""DisablePreemptionOnS3S4"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""ComputePreemption"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""DisableWriteCombining"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DesktopStereoShortcuts"" /t REG_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""FeatureControl"" /t REG_DWORD /d ""4"" /f
";

            ExecuteBatchCommands(batchCommands);
        }
        private void NvidiaHidden()
        {
            string batchCommands = @"Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""TCCSupported"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKCU\SOFTWARE\NVIDIA Corporation\Global\NVTweak\Devices\509901423-0\Color"" /v ""NvCplUseColorCorrection"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v EnableRID73779  /t REG_DWORD /d 1 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v EnableRID73780  /t REG_DWORD /d 1 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v EnableRID74361  /t REG_DWORD /d 1 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v EnableRID44231  /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v EnableRID64640  /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v EnableRID66610  /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client"" /v OptInOrOutPreference /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\services\NvTelemetryContainer"" /v Start /t REG_DWORD /d 4 /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\Startup"" /v SendTelemetryData /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\Global\Startup\SendTelemetryData"" /v 0 /t REG_DWORD /d 0 /f
Reg.exe add ""HKLM\SOFTWARE\Microsoft\Windows\Dwm"" /v ""OverlayTestMode"" /t REG_DWORD /d ""5"" /f
Reg.exe add ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""EnableMidBufferPreemption"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""RMDisablePostL2Compression"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""RmDisableRegistryCaching"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""DesktopStereoShortcuts"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""FeatureControl"" /t REG_DWORD /d ""4"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""NVDeviceSupportKFilter"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmCacheLoc"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmDisableInst2Sys"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmFbsrPagedDMA"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RMGpuId"" /t REG_DWORD /d ""256"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""RmProfilingAdminOnly"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""TCCSupported"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""TrackResetEngine"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""UseBestResolution"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000"" /v ""ValidateBlitSubRects"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""MaxIAverageGraphicsLatencyInOneBucket"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Scheduler"" /v ""PlatformSupportMiracast"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""CsEnabled"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""PerfCalculateActualUtilization"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""SleepReliabilityDetailedDiagnostics"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""EventProcessorEnabled"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""QosManagesIdleProcessors"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""DisableVsyncLatencyUpdate"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""DisableSensorWatchdog"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""InterruptSteeringDisabled"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\intelppm\Parameters"" /v ""AcpiFirmwareWatchDog"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\intelppm\Parameters"" /v ""AmliWatchdogAction"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\intelppm\Parameters"" /v ""AmliWatchdogTimeout"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\intelppm\Parameters"" /v ""WatchdogTimeout"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Throttle"" /v ""PerfEnablePackageIdle"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""0x112493bd"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""0x11e91a61"" /t REG_DWORD /d ""4294967295"" /f
Reg.exe add ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""DisableCudaContextPreemption"" /t REG_DWORD /d ""1"" /f
for /L %%i in (0,1,9) do (
    for /F ""tokens=2* skip=2"" %%a in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000%%i"" /v ""ProviderName"" 2^>nul') do (
	if /i ""%%b""==""NVIDIA"" (
		set G=000%%i
		)
	)
)
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""MaxPerfWithPerfMon"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmOptp2LowerMclk"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmOverrideIdleSlowdownSettings"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""ThermalPolicySW1"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrCacheStatsOnD3"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrFgRppg"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMD3Feature"" /t REG_DWORD /d ""2"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMElcg"" /t REG_DWORD /d ""1431655765"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RM2644249"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMBlcg"" /t REG_DWORD /d ""286331153"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMElpg"" /t REG_DWORD /d ""4095"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMSlcg"" /t REG_DWORD /d ""262143"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMClkSlowDown"" /t REG_DWORD /d ""71303168"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMFspg"" /t REG_DWORD /d ""15"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmThermalCacheDisable"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmDisableACPI"" /t REG_DWORD /d ""1023"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMNativePcieL1WarFlags"" /t REG_DWORD /d ""16"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RM303107"" /t REG_DWORD /d ""16"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmOverrideSupportChipsetAspm"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMEnableASPMAtLoad"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMEnableASPMPublicBits"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMResetPerfMonD4"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmFbsrWDDMMode"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmFbsrFileMode"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RM592311"" /t REG_DWORD /d ""2"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDisableEDC"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMElpgStateOnInit"" /t REG_DWORD /d ""3"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmPgCtrlDiParameters"" /t REG_DWORD /d ""21"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmThermPolicyOverride"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmThermPolicySwSlowdownOverride"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmGpsACPIType"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmGpsPowerSteeringEnable"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmGpsCpuUtilPoll"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmMIONoPowerOff"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMNvLinkControlLinkPM"" /t REG_DWORD /d ""170"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmEnableNoiseAwarePll"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDisableOptimalPowerForPadlinkPll"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMPexPowerSavings"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RM2779240"" /t REG_DWORD /d ""5"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RM2644249"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmClkPowerOffDramPllWhenUnused"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMOPSB"" /t REG_DWORD /d ""10914"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""DisableDynamicPstate"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDidleFeatureGC5"" /t REG_DWORD /d ""44731050"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDisableGpuASPMFlags"" /t REG_DWORD /d ""3"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmPgCtrlDiParameters"" /t REG_DWORD /d ""21"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""SlideMCLK"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMForceRtd3D3Hot"" /t REG_DWORD /d ""2"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMNvlinkUPHYInitControl"" /t REG_DWORD /d ""16"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmGpsGenoa"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RM580312"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMEnableASPMDT"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMBug2519005War"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmCeElcgWar1895530"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmWar1760398"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMLpwrArch"" /t REG_DWORD /d ""349525"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMLpwrEiClient"" /t REG_DWORD /d ""5"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrCtrlMsDifrCgParameters"" /t REG_DWORD /d ""1365"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmDwbMscg"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmPgCtrlParameters"" /t REG_DWORD /d ""1431655765"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrFgRppg"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMUsePmuSpi"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmDisableInforomBBX"" /t REG_DWORD /d ""15"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmSec2EnableApm"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrGrPgSwFilterFunction"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMGpuOperationMode"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrCtrlGrRgParameters"" /t REG_DWORD /d ""89478485"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmPgCtrlGrParameters"" /t REG_DWORD /d ""1431655765"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrCtrlMsLtcParameters"" /t REG_DWORD /d ""5"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLpwrCtrlMsDifrSwAsrParameters"" /t REG_DWORD /d ""5461"" /f
cls
for /L %%i in (0,1,9) do (
    for /F ""tokens=2* skip=2"" %%a in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000%%i"" /v ""ProviderName"" 2^>nul') do (
	if /i ""%%b""==""NVIDIA"" (
		set G=000%%i
		)
	)
)
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMHdcpKeyglobZero"" /t REG_DWORD /d ""1"" /f > nul 2>&1
cls
echo HDCP succesfully disabled.
cls
for /L %%i in (0,1,9) do (
    for /F ""tokens=2* skip=2"" %%a in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000%%i"" /v ""ProviderName"" 2^>nul') do (
	if /i ""%%b""==""NVIDIA"" (
		set G=000%%i
		)
	)
)
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMNoECCFuseCheck"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMEnableL1ECC"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMEnableSMECC"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMEnableSHMECC"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMAssertOnEccErrors"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RM1441072"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMGuestECCState"" /t REG_DWORD /d ""0"" /f
nvidia-smi.exe -e 0
cls
rmdir /s /q ""%systemdrive%\Windows\System32\drivers\NVIDIA Corporation"" >nul 2>&1
cd /d ""%systemdrive%\Windows\System32\DriverStore\FileRepository\"" >nul 2>&1
dir NvTelemetry64.dll /a /b /s >nul 2>&1
del NvTelemetry64.dll /a /s >nul 2>&1
cd /d ""%systemdrive%\Windows\System32\DriverStore\FileRepository\nv_dispig.inf_amd64_20ea7d0c917cde22"" >nul 2>&1
del NvTelemetry64.dll /a /s >nul 2>&1
rd /s /q ""%systemdrive%\Program Files\NVIDIA Corporation\Display.NvContainer\plugins\LocalSystem\DisplayDriverRAS"" >nul 2>&1
rd /s /q ""%systemdrive%\Program Files\NVIDIA Corporation\DisplayDriverRAS"" >nul 2>&1
rd /s /q ""%systemdrive%\ProgramData\NVIDIA Corporation\DisplayDriverRAS"" >nul 2>&1
Reg.exe add ""HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client"" /v ""OptInOrOutPreference"" /t REG_DWORD /d 0 /f  >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\Startup"" /v ""SendTelemetryData"" /t REG_DWORD /d 0 /f >nul 2>&1
for %%i in (NvTmMon NvTmRep) do (for /f ""tokens=1 delims=,"" %%a in ('schtasks /query /fo csv ^| findstr /v ""TaskName"" ^| findstr ""%%~i"" ^| findstr /v ""Microsoft\\Windows""') do (schtasks /change /tn %%a /disable))
sc config NvTelemetryContainer start=disabled >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\Startup\SendTelemetryData"" /ve /t REG_DWORD /d ""0"" /f >nul 2>&1
cls
echo Telemetry succesfully deleted
cls
for /L %%i in (0,1,9) do (
    for /F ""tokens=2* skip=2"" %%a in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000%%i"" /v ""ProviderName"" 2^>nul') do (
	if /i ""%%b""==""NVIDIA"" (
		set G=000%%i
		)
	)
)
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmPowerFeature"" /t REG_DWORD /d ""1413829973"" /f
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmPowerFeature2"" /t REG_DWORD /d ""89478485"" /f
cls
SETLOCAL EnableDelayedExpansion
title Disable Write Combining
cls

for /L %%i in (0,1,9) do (
    for /F ""tokens=2* skip=2"" %%a in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000%%i"" /v ""ProviderName"" 2^>nul') do (
	if /i ""%%b""==""NVIDIA"" (
		set G=000%%i
		)
	)
)
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmForceDisableIomapWC"" /t REG_DWORD /d ""1"" /f
cls
or /L %%i in (0,1,9) do (
    for /F ""tokens=2* skip=2"" %%a in ('reg query ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000%%i"" /v ""ProviderName"" 2^>nul') do (
	if /i ""%%b""==""NVIDIA"" (
		set G=000%%i
		)
	)
)
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMCtxswLog"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""PciLatencyTimerControl"" /t REG_DWORD /d ""32"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMPcieLtrL12ThresholdOverride"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMPcieLtrOverride"" /t REG_DWORD /d ""2"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""SkipSwStateErrChecks"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMSuppressGPIOIntrErrLog"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMAERRForceDisable"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMNvLog"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDisablePerIntrDPCQueueing"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMGC6Feature"" /t REG_DWORD /d ""699050"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMGC6Parameters"" /t REG_DWORD /d ""85"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMHotPlugSupportDisable"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmFbsrPagedDMA"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDisablePostL2Compression"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmRcWatchdog"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmLogonRC"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMIntrDetailedLogs"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMUsbcDebugMode"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmDisableInforomNvlink"" /t REG_DWORD /d ""3"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDisableFeatureDisablement"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmBreakonRC"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDebugSetSMCMode"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMDisableLRCCoalescing"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmGpsPsEnablePerCpuCoreDpc"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmEnableI2CNanny"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMBandwidthFeature"" /t REG_DWORD /d ""1896072192"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMBandwidthFeature2"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RmDisablePreosapps"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\%G%"" /v ""RMEnableEventTracer"" /t REG_DWORD /d ""0"" /f
nvidia-smi.exe -acp 0
cls
";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertNvidiaHidden()
        {
            string batchCommands = @"set cdCache=%cd%
cd ""%SystemDrive%\Program Files\NVIDIA Corporation\NVSMI\""
start "" /I /WAIT /B ""nvidia-smi"" -acp 0
cd %cdCache%

reg delete ""HKCU\Software\NVIDIA Corporation\Global\NVTweak\Devices\509901423-0\Color"" /v ""NvCplUseColorCorrection"" /t Reg_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""PlatformSupportMiracast"" /t Reg_DWORD /d ""0"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\Global\NVTweak"" /v ""DisplayPowerSaving"" /t Reg_DWORD /d ""0"" /f

cd ""%SYSTEMDRIVE%\Program Files\NVIDIA Corporation\NVSMI\""
nvidia-smi -acp UNRESTRICTED
nvidia-smi -acp DEFAULT

for /f %a in ('reg query ""HKLM\System\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"" /t REG_SZ /s /e /f ""NVIDIA"" ^| findstr ""HKEY""') do (
    echo !S_MAGENTA! Reset Tiled Display!S_YELLOW! 
timeout /t 1 /nobreak > NUL
    reg delete ""%a"" /v ""EnableTiledDisplay"" /f
    echo !S_MAGENTA! Reset TCC!S_YELLOW! 
timeout /t 1 /nobreak > NUL
    reg delete ""%a"" /v ""TCCSupported"" /f
)


for /f %i in ('reg query ""HKLM\System\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"" /t REG_SZ /s /e /f ""NVIDIA"" ^| findstr ""HKEY""') do (
  Reg delete ""%a"" /v ""PowerMizerEnable"" /t REG_DWORD /d ""1"" /f
  Reg delete ""%a"" /v ""PowerMizerLevel"" /t REG_DWORD /d ""1"" /f
  Reg delete ""%a"" /v ""PowerMizerLevelAC"" /t REG_DWORD /d ""1"" /f
  Reg delete ""%a"" /v ""PerfLevelSrc"" /t REG_DWORD /d ""8738"" /f
)

reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm\FTS"" /v ""EnableRID61684"" /t REG_DWORD /d ""1"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\nvlddmkm"" /v ""DisableWriteCombining"" /t Reg_DWORD /d ""1"" /f

reg delete ""HKLM\SOFTWARE\NVIDIA Corporation\NvControlPanel2\Client"" /v ""OptInOrOutPreference"" /t REG_DWORD /d 0 /f
reg delete ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v ""EnableRID44231"" /t REG_DWORD /d 0 /f
reg delete ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v ""EnableRID64640"" /t REG_DWORD /d 0 /f
reg delete ""HKLM\SOFTWARE\NVIDIA Corporation\Global\FTS"" /v ""EnableRID66610"" /t REG_DWORD /d 0 /f
reg delete ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"" /v ""NvBackend"" /f
schtasks /change /disable /tn ""NvTmRep_CrashReport1_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}""
schtasks /change /disable /tn ""NvTmRep_CrashReport2_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}""
schtasks /change /disable /tn ""NvTmRep_CrashReport3_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}""
schtasks /change /disable /tn ""NvTmRep_CrashReport4_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}""
sc config NvTelemetyContainer start=disabled
sc stop NvTelemetyContainer
if exist ""%systmedrive%\Program Files\NVIDIA Corporation\Installer2\InstallerCore\NVI2.DLL"" (rundll32 ""%systmedrive%\Program Files\NVIDIA Corporation\Installer2\InstallerCore\NVI2.DLL"",UninstallPackage NvTelemetryContainer)

Reg.exe delete ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""LogWarningEntries"" /t REG_DWORD /d ""0"" /f
Reg.exe delete ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""LogPagingEntries"" /t REG_DWORD /d ""0"" /f
Reg.exe delete ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""LogEventEntries"" /t REG_DWORD /d ""0"" /f
Reg.exe delete ""HKLM\SYSTEM\ControlSet001\Services\nvlddmkm"" /v ""LogErrorEntries"" /t REG_DWORD /d ""0"" /f

sc config NVDisplay.ContainerLocalSystem start= auto
sc start NVDisplay.ContainerLocalSystem
";

            ExecuteBatchCommands(batchCommands);
        }
        private void NvidiaPStates()
        {
            string batchCommands = @"for /f %f in ('wmic path Win32_VideoController get PNPDeviceID^| findstr /L ""PCI\VEN_""') do (
	for /f ""tokens=3"" %b in ('reg query ""HKLM\SYSTEM\ControlSet001\Enum\%f"" /v ""Driver""') do (
		for /f %f in ('echo %b ^| findstr ""{""') do (
		     Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Class\%f"" /v ""DisableDynamicPstate"" /t REG_DWORD /d ""1"" /f
                   )
                )
             )
			 for /f %m in ('wmic path Win32_VideoController get PNPDeviceID^| findstr /L ""PCI\VEN_""') do (
	for /f ""tokens=3"" %a in ('reg query ""HKLM\SYSTEM\ControlSet001\Enum\%m"" /v ""Driver""') do (
		for /f %m in ('echo %a ^| findstr ""{""') do (
		     Reg.exe add ""HKLM\System\ControlSet001\Control\Class\%m"" /v ""RMHdcpKeyglobZero"" /t REG_DWORD /d ""1"" /f
                   )
                )
             )
";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertNvidiaPStates()
        {
            string batchCommands = @"for /f %i in ('reg query ""HKLM\System\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"" /t REG_SZ /s /e /f ""NVIDIA"" ^| findstr ""HKEY""') do (
  reg add ""%i"" /v ""DisableDynamicPstate"" /t REG_DWORD /d ""0"" /f
)
";

            ExecuteBatchCommands(batchCommands);
        }
        private void TweakedNip()
        {
            string batchCommands = @"if not exist ""C:\ToX\"" ( mkdir ""C:\ToX"" >nul 2>&1 )
if not exist ""C:\ToX\Resources\"" ( mkdir ""C:\ToX\Resources\"" >nul 2>&1 )
curl -g -k -L -# -o ""%tmp%\nvidiaProfileInspector.zip"" ""https://github.com/Orbmu2k/nvidiaProfileInspector/releases/latest/download/nvidiaProfileInspector.zip"">nul 2>&1
powershell -NoProfile Expand-Archive '%tmp%\nvidiaProfileInspector.zip' -DestinationPath 'C:\ToX\Resources\'>nul 2>&1
curl -g -k -L -# -o ""C:\ToX\Resources\ToXFree.nip"" ""https://raw.githubusercontent.com/ToXTweaks/Resources-Free/refs/heads/main/ToXFree.nip"" >nul 2>&1
start "" /D ""C:\ToX\Resources"" nvidiaProfileInspector.exe ToXFree.nip";

            ExecuteBatchCommands(batchCommands);
        }
        private void RevertNip()
        {
            string batchCommands = @"if not exist ""C:\ToX\"" ( mkdir ""C:\ToX"" >nul 2>&1 )
if not exist ""C:\ToX\Resources\"" ( mkdir ""C:\ToX\Resources\"" >nul 2>&1 )
curl -g -k -L -# -o ""%tmp%\nvidiaProfileInspector.zip"" ""https://github.com/Orbmu2k/nvidiaProfileInspector/releases/latest/download/nvidiaProfileInspector.zip"">nul 2>&1
powershell -NoProfile Expand-Archive '%tmp%\nvidiaProfileInspector.zip' -DestinationPath 'C:\ToX\Resources\'>nul 2>&1
curl -g -k -L -# -o ""C:\ToX\Resources\Basic.nip"" ""https://www.dropbox.com/scl/fi/jjd47lkw1zyh0znp8lheh/Basics.nip?rlkey=fdnxdmt5gw7xdxgyb2l9w5wed&st=2yz2eids&dl=1"" >nul 2>&1
start "" /D ""C:\ToX\Resources"" nvidiaProfileInspector.exe Basic.nip";

            ExecuteBatchCommands(batchCommands);
        }
        private void ExecuteBatchCommands(string batchCommands)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Verb = "runas"
            };

            using (Process process = Process.Start(processInfo))
            {
                using (StreamWriter sw = process.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(batchCommands);
                    }
                }
                process.WaitForExit();
            }
        }

    }
}