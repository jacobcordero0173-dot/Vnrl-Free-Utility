using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToX_Free_Utility.LoadingForms;

namespace ToX_Free_Utility.Tabs
{
    public partial class WindowsTweaks : Form
    {
        public WindowsTweaks()
        {
            InitializeComponent();
            InitializeComponents();
            LoadSettings();
        }
        #region -
        public void InitializeComponents()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;

            string exeName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);

            if (!appName.Equals("ToX Free Utility", StringComparison.OrdinalIgnoreCase))
            {
                Environment.Exit(0);
            }

            if (!exeName.Equals("ToX Free Utility.exe", StringComparison.OrdinalIgnoreCase))
            {
                Environment.Exit(0);
            }

            if (!this.Text.Contains("ToX Free Utility"))
            {
                Environment.Exit(0);
            }
        }
        #endregion
        private void LoadSettings()
        {
            BasicPrivacy.Checked = Properties.Settings.Default.BasicPrivacy;
            BasicPerf.Checked = Properties.Settings.Default.BasicPerf;
            GameBar.Checked = Properties.Settings.Default.GameBar;
            DFeatures.Checked = Properties.Settings.Default.DFeatures;
            PowerPlan.Checked = Properties.Settings.Default.PowerPlan;
            DCortana.Checked = Properties.Settings.Default.DCortana;
        }

        private void SaveState(Guna2ToggleSwitch Tswitch, string Tbool)
        {
            Properties.Settings.Default[Tbool] = Tswitch.Checked;
            Properties.Settings.Default.Save();
        }

        private void BasicPrivacy_CheckedChanged(object sender, EventArgs e) => SaveState(BasicPrivacy, "BasicPrivacy");
        private void BasicPerf_CheckedChanged(object sender, EventArgs e) => SaveState(BasicPerf, "BasicPerf");
        private void GameBar_CheckedChanged(object sender, EventArgs e) => SaveState(GameBar, "GameBar");
        private void DFeatures_CheckedChanged(object sender, EventArgs e) => SaveState(DFeatures, "DFeatures");
        private void PowerPlan_CheckedChanged(object sender, EventArgs e) => SaveState(PowerPlan, "PowerPlan");
        private void DCortana_CheckedChanged(object sender, EventArgs e) => SaveState(DCortana, "DCortana");

        private async void BasicPrivacy_Click(object sender, EventArgs e)
        {
            if (BasicPrivacy.Checked) await RunWithLoadingScreenAsync(ApplyBasicPrivacy);
            else RevertBasicPrivacy();
        }

        private async void BasicPerf_Click(object sender, EventArgs e)
        {
            if (BasicPerf.Checked) await RunWithLoadingScreenAsync(ApplyBasicPerformance);
            else RevertBasicPerformance();
        }

        private async void GameBar_Click(object sender, EventArgs e)
        {
            if (GameBar.Checked) await RunWithLoadingScreenAsync(DisableGameBar);
            else RevertDisableGameBar();
        }

        private async void DFeatures_Click(object sender, EventArgs e)
        {
            if (DFeatures.Checked) await RunWithLoadingScreenAsync(DisableUselessFeatures);
            else RevertDisableUselessFeatures();
        }

        private async void PowerPlan_Click(object sender, EventArgs e)
        {
            if (PowerPlan.Checked) await RunWithLoadingScreenAsync(ImportPowerPlan);
            else RevertImportPowerPlan();
        }

        private async void DCortana_Click(object sender, EventArgs e)
        {
            if (DCortana.Checked) await RunWithLoadingScreenAsync(DisableCortana);
            else RevertDisableCortana();
        }

        private async Task RunWithLoadingScreenAsync(Action tweakAction)
        {
            TweaksLoading loadingForm = new TweaksLoading
            {
                StartPosition = FormStartPosition.CenterParent,
                TopMost = true
            };

            Main mainForm = (Main)this.ParentForm;
            TweaksLoading.ShowModal(mainForm, loadingForm);

            await Task.Delay(2000);
            await Task.Run(tweakAction);

            loadingForm.CloseModal();

            TweaksSuccess successForm = new TweaksSuccess
            {
                StartPosition = FormStartPosition.CenterParent,
                TopMost = true
            };
            await TweaksSuccess.ShowModal(mainForm, successForm);
        }

        private void ApplyBasicPrivacy()
        {
            string batchCommands = @"- Basic Privacy Tweaks -

1. Windows Error Reporting
sc stop WerSvc
sc config WerSvc start= disabled
reg add ""HKCU\Control Panel\International\User Profile"" /v ""HttpAcceptLanguageOptOut"" /t REG_DWORD /d ""1"" /f
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost"" /v ""EnableWebContentEvaluation"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SOFTWARE\Microsoft\Windows\Windows Error Reporting"" /v ""DontShowUI"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\PCHealth\ErrorReporting"" /v ""DoReport"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo"" /v ""DisabledByGroupPolicy"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports"" /v ""PreventHandwritingErrorReports"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\TabletPC"" /v ""PreventHandwritingDataSharing"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""AutoApproveOSDumps"" /t REG_DWORD /d 0 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""Disabled"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""DontSendAdditionalData"" /t REG_DWORD /d 1 /f
reg add ""HKLM\Software\Microsoft\PCHealth\ErrorReporting"" /v ""ShowUI"" /t REG_DWORD /d ""0"" /f
reg add ""HKCU\Control Panel\International\User Profile"" /v ""HttpAcceptLanguageOptOut"" /t REG_DWORD /d ""1"" /f
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost"" /v ""EnableWebContentEvaluation"" /t REG_DWORD /d ""0"" /f
reg add ""HKCU\SOFTWARE\Microsoft\Windows\Windows Error Reporting"" /v ""DontShowUI"" /t REG_DWORD /d ""1"" /f
reg add ""HKCU\Software\Microsoft\Windows\Windows Error Reporting"" /v ""Disabled"" /t REG_DWORD /d ""1"" /f
reg add ""HKCU\Software\Microsoft\Windows\Windows Error Reporting"" /v ""DontSendAdditionalData"" /t REG_DWORD /d ""1"" /f
reg add ""HKCU\Software\Microsoft\Windows\Windows Error Reporting"" /v ""LoggingDisabled"" /t REG_DWORD /d ""1"" /f
reg add ""HKCU\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultConsent"" /t REG_DWORD /d ""0"" /f
reg add ""HKCU\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultOverrideBehavior"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SOFTWARE\Microsoft\Windows\Windows Error Reporting"" /v ""DontShowUI"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\PCHealth\ErrorReporting"" /v ""DoReport"" /t REG_DWORD /d ""0"" /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo"" /v ""DisabledByGroupPolicy"" /t REG_DWORD /d ""1"" /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports"" /v ""PreventHandwritingErrorReports"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\TabletPC"" /v ""PreventHandwritingDataSharing"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""AutoApproveOSDumps"" /t REG_DWORD /d 0 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""Disabled"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""DontSendAdditionalData"" /t REG_DWORD /d 1 /f
reg add ""HKLM\SYSTEM\CurrentControlSet\Services\W3SVC"" /v Start /t REG_DWORD /d 0 /f
reg add ""HKLM\Software\Microsoft\PCHealth\ErrorReporting"" /v ""ShowUI"" /t REG_DWORD /d ""0"" /f


Boot Config Tweaks
bcdedit /set disabledynamictick yes     
bcdedit /set useplatformtick yes     
bcdedit /deletevalue useplatformclock     
bcdedit /set hypervisorlaunchtype off


DMA Remapping
@echo disabledmaremapping
SETLOCAL EnableDelayedExpansion
Reg.exe add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\DmaGuard\DeviceEnumerationPolicy"" /v ""value"" /t REG_DWORD /d ""2"" /f >nul 2>&1
for /f %%i in ('reg query ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services"" /s /f DmaRemappingCompatible ^| find /i ""Services\"" ') do (
	echo %%i...
	Reg.exe add ""%%i"" /v ""DmaRemappingCompatible"" /t REG_DWORD /d ""0"" /f >nul 2>&1
) >nul 2>&1

Mitigations
@echo Disablemitigations
SETLOCAL EnableDelayedExpansion
powershell ""ForEach($v in (Get-Command -Name \""Set-ProcessMitigation\"").Parameters[\""Disable\""].Attributes.ValidValues){Set-ProcessMitigation -SYSTEM -Disable $v.ToString() -ErrorAction SilentlyContinue}"" >nul 2>&1
for /f ""tokens=3 skip=2"" %%a in ('reg query ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MitigationAuditOptions""') do set mitigation_mask=%%a
for /L %%a in (0,1,9) do (
    set ""mitigation_mask=!mitigation_mask:%%a=2!
)
for %%a in (
	fontdrvhost.exe
	dwm.exe
	lsass.exe
	svchost.exe
	WmiPrvSE.exe
	winlogon.exe
	csrss.exe
	audiodg.exe
	ntoskrnl.exe
	services.exe
) do (
	Reg.exe add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\%%a"" /v ""MitigationOptions"" /t REG_BINARY /d ""!mitigation_mask!"" /f >nul 2>&1
	Reg.exe add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\%%a"" /v ""MitigationAuditOptions"" /t REG_BINARY /d ""!mitigation_mask!"" /f >nul 2>&1
)
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MitigationOptions"" /t REG_BINARY /d ""!mitigation_mask!"" /f >nul 2>&1
Reg.exe add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""MitigationAuditOptions"" /t REG_BINARY /d ""!mitigation_mask!"" /f >nul 2>&1
reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager"" /v ""ProtectionMode"" /t REG_DWORD /d ""0"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableExceptionChainValidation"" /t REG_DWORD /d ""1"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableControlFlowGuardXfg"" /t REG_DWORD /d ""1"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel"" /v ""DisableControlFlowGuardExportSuppression"" /t REG_DWORD /d ""1"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettingsOverride"" /t REG_DWORD /d ""3"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettingsOverrideMask"" /t REG_DWORD /d ""3"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"" /v ""FeatureSettings"" /t REG_DWORD /d ""1"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\SCMConfig"" /v ""EnableSvchostMitigationsPolicy"" /t REG_DWORD /d ""0"" /f >nul 2>&1
Reg.exe add ""HKLM\SYSTEM\ControlSet001\Control\SCMConfig"" /v ""EnableSvchostMitigationsPolicy"" /t REG_DWORD /d ""0"" /f >nul 2>&1

2. Windows Settings Sync
Reg add ""HKLM\Software\Policies\Microsoft\Windows\SettingSync"" /v ""DisableSettingSync"" /t REG_DWORD /d ""2"" /f
Reg add ""HKLM\Software\Policies\Microsoft\Windows\SettingSync"" /v ""DisableSettingSyncUserOverride"" /t REG_DWORD /d ""1"" /f
Reg add ""HKLM\Software\Policies\Microsoft\Windows\SettingSync"" /v ""DisableSyncOnPaidNetwork"" /t REG_DWORD /d ""1"" /f

3. Location Tracking
Reg add ""HKLM\Software\Policies\Microsoft\FindMyDevice"" /v ""AllowFindMyDevice"" /t REG_DWORD /d ""0"" /f
Reg add ""HKLM\Software\Policies\Microsoft\FindMyDevice"" /v ""LocationSyncEnabled"" /t REG_DWORD /d ""0"" /f

4. Speech Privacy
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Speech_OneCore\Settings\OnlineSpeechPrivacy"" /v ""HasAccepted"" /t REG_DWORD /d 00000000 /f

5. Diagnostics & Feedback
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack"" /v ""ShowedToastAtLevel"" /t REG_DWORD /d 00000001 /f
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection"" /v ""AllowTelemetry"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy"" /v ""TailoredExperiencesWithDiagnosticDataEnabled"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack\EventTranscriptKey"" /v ""EnableEventTranscript"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Siuf\Rules"" /v ""NumberOfSIUFInPeriod"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Siuf\Rules"" /v ""PeriodInNanoSeconds"" /t REG_SZ /d ""-"" /f

6. Activity History
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System"" /v ""PublishUserActivities"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System"" /v ""UploadUserActivities"" /t REG_DWORD /d 00000000 /f

7. Disable Notifications, Location, App Diagnostics, Account Info ACcess
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userNotificationListener"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appDiagnostics"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userAccountInformation"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications"" /v ""ToastEnabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""NOC_GLOBAL_SETTING_ALLOW_NOTIFICATION_SOUND"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""NOC_GLOBAL_SETTING_ALLOW_CRITICAL_TOASTS_ABOVE_LOCK"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\QuietHours"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\windows.immersivecontrolpanel_cw5n1h2txyewy!microsoft.windows.immersivecontrolpanel"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.AutoPlay"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.LowDisk"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.Print.Notification"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.WiFiNetworkManager"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Policies\Microsoft\Windows\Explorer"" /v ""DisableNotificationCenter"" /t REG_DWORD /d ""1"" /f

8. Disable Automatic Maintanance
reg add ""HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\Maintenance"" /v ""MaintenanceDisabled"" /t REG_DWORD /d ""1"" /f 

9. Disable Settings Synchronization
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\AppSync"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\DesktopTheme"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\PackageState"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\StartLayout"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
reg add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f 
";

            ExecuteBatchCommands(batchCommands);
        }

        private void RevertBasicPrivacy()
        {
            string batchCommands = @"- Basic Privacy Tweaks -

1. Windows Error Reporting
sc stop WerSvc
sc config WerSvc start= disabled
reg delete ""HKCU\Control Panel\International\User Profile"" /v ""HttpAcceptLanguageOptOut"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo"" /v ""Enabled"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost"" /v ""EnableWebContentEvaluation"" /f
reg delete ""HKLM\SOFTWARE\Microsoft\Windows\Windows Error Reporting"" /v ""DontShowUI"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\PCHealth\ErrorReporting"" /v ""DoReport"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo"" /v ""DisabledByGroupPolicy"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports"" /v ""PreventHandwritingErrorReports"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\TabletPC"" /v ""PreventHandwritingDataSharing"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""AutoApproveOSDumps"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""Disabled"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""DontSenddeleteitionalData"" /f
reg delete ""HKLM\Software\Microsoft\PCHealth\ErrorReporting"" /v ""ShowUI"" /f
reg delete ""HKCU\Control Panel\International\User Profile"" /v ""HttpAcceptLanguageOptOut"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo"" /v ""Enabled"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost"" /v ""EnableWebContentEvaluation"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\Windows Error Reporting"" /v ""DontShowUI"" /f
reg delete ""HKCU\Software\Microsoft\Windows\Windows Error Reporting"" /v ""Disabled"" /f
reg delete ""HKCU\Software\Microsoft\Windows\Windows Error Reporting"" /v ""DontSenddeleteitionalData"" /f
reg delete ""HKCU\Software\Microsoft\Windows\Windows Error Reporting"" /v ""LoggingDisabled"" /f
reg delete ""HKCU\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultConsent"" /f
reg delete ""HKCU\Software\Microsoft\Windows\Windows Error Reporting\Consent"" /v ""DefaultOverrideBehavior"" /f
reg delete ""HKLM\SOFTWARE\Microsoft\Windows\Windows Error Reporting"" /v ""DontShowUI"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\PCHealth\ErrorReporting"" /v ""DoReport"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo"" /v ""DisabledByGroupPolicy"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports"" /v ""PreventHandwritingErrorReports"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\TabletPC"" /v ""PreventHandwritingDataSharing"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""AutoApproveOSDumps"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""Disabled"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting"" /v ""DontSenddeleteitionalData"" /f
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\W3SVC"" /v Start /f
reg delete ""HKLM\Software\Microsoft\PCHealth\ErrorReporting"" /v ""ShowUI"" /f

2. Windows Settings Sync
reg delete ""HKLM\Software\Policies\Microsoft\Windows\SettingSync"" /v ""DisableSettingSync"" /f
reg delete ""HKLM\Software\Policies\Microsoft\Windows\SettingSync"" /v ""DisableSettingSyncUserOverride"" /f
reg delete ""HKLM\Software\Policies\Microsoft\Windows\SettingSync"" /v ""DisableSyncOnPaidNetwork"" /f

3. Location Tracking
reg delete ""HKLM\Software\Policies\Microsoft\FindMyDevice"" /v ""AllowFindMyDevice"" /f
reg delete ""HKLM\Software\Policies\Microsoft\FindMyDevice"" /v ""LocationSyncEnabled"" /f

4. Speech Privacy
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Speech_OneCore\Settings\OnlineSpeechPrivacy"" /v ""HasAccepted"" /f

5. Diagnostics & Feedback
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack"" /v ""ShowedToastAtLevel"" /f
reg delete ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection"" /v ""AllowTelemetry"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy"" /v ""TailoredExperiencesWithDiagnosticDataEnabled"" /f
reg delete ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack\EventTranscriptKey"" /v ""EnableEventTranscript"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Siuf\Rules"" /v ""NumberOfSIUFInPeriod"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Siuf\Rules"" /v ""PeriodInNanoSeconds"" /f

6. Activity History
reg delete ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System"" /v ""PublishUserActivities"" /f
reg delete ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System"" /v ""UploadUserActivities"" /f

7. Disable Notifications, Location, App Diagnostics, Account Info ACcess
reg delete ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userNotificationListener"" /v ""Value"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location"" /v ""Value"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appDiagnostics"" /v ""Value"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userAccountInformation"" /v ""Value"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications"" /v ""ToastEnabled"" /f 
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""NOC_GLOBAL_SETTING_ALLOW_NOTIFICATION_SOUND"" /f 
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""NOC_GLOBAL_SETTING_ALLOW_CRITICAL_TOASTS_ABOVE_LOCK"" /f 
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""QuietHoursEnabled"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""Windows.SystemToast.WiFiNetworkManager"" /f 
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""Windows.SystemToast.Print.Notification"" /f 
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings"" /v ""Windows.SystemToast.LowDisk"" /f

8. Disable Automatic Maintenance
reg delete ""HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\Maintenance"" /v ""MaintenanceDisabled"" /f

9. Disable Settings Synchronization
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility"" /v ""Enabled"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\AppSync"" /v ""Enabled"" /f
";

            ExecuteBatchCommands(batchCommands);
        }

        private void ApplyBasicPerformance()
        {
            string batchCommands = @"- General Performance Tweaks -

1. Enable Game Mode
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\GameBar"" /v ""AllowAutoGameMode"" /t REG_DWORD /d 00000001 /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\GameBar"" /v ""AutoGameModeEnabled"" /t REG_DWORD /d 00000001 /f

2. Disable Hardware Accelerated GPU Scheduling
reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""HwSchMode"" /t REG_DWORD /d 00000004 /f

3. Disable Variable Refresh Rate
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\DirectX\UserGpuPreferences"" /v ""DirectXUserGlobalSettings"" /t REG_SZ /d ""VRROptimizeEnable=0;"" /f

4. Set Win32Priorit Separation
reg add ""HKLM\SYSTEM\CurrentControlSet\Control\PriorityControl"" /v ""Win32PrioritySeparation"" /t REG_DWORD /d ""42"" /f

5. Disable PowerThrottling
Reg add ""HKLM\SYSTEM\CurrentControlSet\Control\Power\PowerThrottling"" /v ""PowerThrottlingOff"" /t REG_DWORD /d ""1"" /f

Reg.exe add ""HKCU\SOFTWARE\Microsoft\Games"" /v ""FpsAll"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKCU\SOFTWARE\Microsoft\Games"" /v ""FpsStatusGames"" /t REG_DWORD /d ""10"" /f
Reg.exe add ""HKCU\SOFTWARE\Microsoft\Games"" /v ""FpsStatusGamesAll"" /t REG_DWORD /d ""4"" /f
Reg.exe add ""HKCU\SOFTWARE\Microsoft\Games"" /v ""GameFluidity"" /t REG_DWORD /d ""1"" /f

sutil behavior set memoryusage 2 >nul 2>&1
fsutil behavior set mftzone 4 >nul 2>&1
fsutil behavior set Disablinglastaccess 1 >nul 2>&1
fsutil behavior set Disablingdeletenotify 0 >nul 2>&1
fsutil behavior set encryptpagingfile 0 >nul 2>&1

Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\DXGKrnl"" /v ""MonitorLatencyTolerance"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Services\DXGKrnl"" /v ""MonitorRefreshLatencyTolerance"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""ExitLatency"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""ExitLatencyCheckEnablingd"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""Latency"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""LatencyToleranceDefault"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""LatencyToleranceFSVP"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""LatencyTolerancePerfOverride"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""LatencyToleranceScreenOffIR"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""LatencyToleranceVSyncEnablingd"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""RtlCapabilityCheckLatency"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultD3TransitionLatencyActivelyUsed"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultD3TransitionLatencyIdleLongTime"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultD3TransitionLatencyIdleMonitorOff"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultD3TransitionLatencyIdleNoContext"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultD3TransitionLatencyIdleShortTime"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultD3TransitionLatencyIdleVeryLongTime"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceIdle0"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceIdle0MonitorOff"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceIdle1"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceIdle1MonitorOff"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceMemory"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceNoContext"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceNoContextMonitorOff"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceOther"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultLatencyToleranceTimerPeriod"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultMemoryRefreshLatencyToleranceActivelyUsed"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultMemoryRefreshLatencyToleranceMonitorOff"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""DefaultMemoryRefreshLatencyToleranceNoContext"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""Latency"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""MaxIAverageGraphicsLatencyInOneBucket"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""MiracastPerfTrackGraphicsLatency"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""MonitorLatencyTolerance"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""MonitorRefreshLatencyTolerance"" /t REG_DWORD /d ""1"" /f 
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power"" /v ""TransitionLatency"" /t REG_DWORD /d ""1"" /f 

Reg.exe add ""HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile"" /v ""SystemResponsiveness"" /t REG_DWORD /d ""0"" /f 
";

            ExecuteBatchCommands(batchCommands);
        }

        private void RevertBasicPerformance()
        {
            string batchCommands = @"- General Performance Tweaks -

1. Disable Game Mode
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\GameBar"" /v ""AllowAutoGameMode"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\GameBar"" /v ""AutoGameModeEnabled"" /f

2. Disable Hardware Accelerated GPU Scheduling
reg delete ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers"" /v ""HwSchMode"" /f

3. Enable Variable Refresh Rate
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\DirectX\UserGpuPreferences"" /v ""DirectXUserGlobalSettings"" /f

4. Reset Win32Priority Separation
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\PriorityControl"" /v ""Win32PrioritySeparation"" /f

5. Enable PowerThrottling
reg delete ""HKLM\SYSTEM\CurrentControlSet\Control\Power\PowerThrottling"" /v ""PowerThrottlingOff"" /f
";

            ExecuteBatchCommands(batchCommands);
        }

        private void DisableGameBar()
        {
            string batchCommands = @"- Disable Game Bar & DVR -

reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\ApplicationManagement\AllowGameDVR"" /v ""value"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\GameDVR"" /v ""AllowGameDVR"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_CURRENT_USER\System\GameConfigStore"" /v ""GameDVR_Enabled"" /t REG_DWORD /d 00000000 /f
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR"" /v ""AppCaptureEnabled"" /t REG_DWORD /d 00000000 /f";

            ExecuteBatchCommands(batchCommands);
        }

        private void RevertDisableGameBar()
        {
            string batchCommands = @"- Disable Game Bar & DVR -

reg delete ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\ApplicationManagement\AllowGameDVR"" /v ""value"" /f
reg delete ""HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\GameDVR"" /v ""AllowGameDVR"" /f
reg delete ""HKEY_CURRENT_USER\System\GameConfigStore"" /v ""GameDVR_Enabled"" /f
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR"" /v ""AppCaptureEnabled"" /f
";

            ExecuteBatchCommands(batchCommands);
        }

        private void DisableUselessFeatures()
        {
            string batchCommands = @"- Disable Useless Features -

1. Disable Wen in Search
Reg add ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""ConnectedSearchUseWeb"" /t REG_DWORD /d ""0"" /f
Reg add ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""DisableWebSearch"" /t REG_DWORD /d ""1"" /f
Reg add ""HKLM\Software\Microsoft\Windows\CurrentVersion\Search"" /v ""BingSearchEnabled"" /t REG_DWORD /d ""0"" /f

2. Disable Transparency
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize"" /v ""EnableTransparency"" /t REG_DWORD /d 00000000 /f

3. Disable Ease of Access Tab
reg add ""HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f
reg add ""HKEY_CURRENT_USER\Control Panel\Accessibility\StickyKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f
reg add ""HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response"" /v ""Flags"" /t REG_SZ /d ""0"" /f
reg add ""HKEY_CURRENT_USER\Control Panel\Accessibility\ToggleKeys"" /v ""Flags"" /t REG_SZ /d ""0"" /f

4. Disable Background Apps in Search
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search"" /v ""BackgroundAppGlobalToggle"" /t REG_DWORD /d 00000000 /f

Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\activity"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appDiagnostics"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appointments"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\bluetoothSync"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\broadFileSystemAccess"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\cellularData"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\cellularData\Microsoft.Win32WebViewHost_cw5n1h2txyewy"" /v ""Value"" /t REG_SZ /d ""Allow"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\chat"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\contacts"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\documentsLibrary"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\email"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\gazeInput"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location\Microsoft.Win32WebViewHost_cw5n1h2txyewy"" /v ""Value"" /t REG_SZ /d ""Prompt"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\phoneCall"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\phoneCallHistory"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\picturesLibrary"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\radios"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userAccountInformation"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userAccountInformation\Microsoft.AccountsControl_cw5n1h2txyewy"" /v ""Value"" /t REG_SZ /d ""Prompt"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userDataTasks"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userNotificationListener"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\videosLibrary"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam"" /v ""Value"" /t REG_SZ /d ""Allow"" /f
Reg Add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\Microsoft.Win32WebViewHost_cw5n1h2txyewy"" /v ""Value"" /t REG_SZ /d ""Allow"" /f

5. Disable Customer Experience Improvement Program
schtasks /end /tn ""\Microsoft\Windows\Customer Experience Improvement Program\Consolidator""
schtasks /change /tn ""\Microsoft\Windows\Customer Experience Improvement Program\Consolidator"" /disable
schtasks /end /tn ""\Microsoft\Windows\Customer Experience Improvement Program\BthSQM""
schtasks /change /tn ""\Microsoft\Windows\Customer Experience Improvement Program\BthSQM"" /disable
schtasks /end /tn ""\Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask""
schtasks /change /tn ""\Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask"" /disable
schtasks /end /tn ""\Microsoft\Windows\Customer Experience Improvement Program\UsbCeip""
schtasks /change /tn ""\Microsoft\Windows\Customer Experience Improvement Program\UsbCeip"" /disable
schtasks /end /tn ""\Microsoft\Windows\Customer Experience Improvement Program\Uploader""
schtasks /change /tn ""\Microsoft\Windows\Customer Experience Improvement Program\Uploader"" /disable
schtasks /end /tn ""\Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser""
schtasks /change /tn ""\Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser"" /disable
schtasks /end /tn ""\Microsoft\Windows\Application Experience\ProgramDataUpdater""
schtasks /change /tn ""\Microsoft\Windows\Application Experience\ProgramDataUpdater"" /disable
schtasks /end /tn ""\Microsoft\Windows\Application Experience\StartupAppTask""
schtasks /end /tn ""\Microsoft\Windows\Shell\FamilySafetyMonitor""
schtasks /change /tn ""\Microsoft\Windows\Shell\FamilySafetyMonitor"" /disable
schtasks /end /tn ""\Microsoft\Windows\Shell\FamilySafetyRefresh""
schtasks /change /tn ""\Microsoft\Windows\Shell\FamilySafetyRefresh"" /disable
schtasks /end /tn ""\Microsoft\Windows\Shell\FamilySafetyUpload""
schtasks /change /tn ""\Microsoft\Windows\Shell\FamilySafetyUpload"" /disable
schtasks /end /tn ""\Microsoft\Windows\Maintenance\WinSAT""

6. Disable Activity Feed
Reg.exe add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Feeds"" /v ""EnableFeeds"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SOFTWARE\Policies\Microsoft"" /v ""AllowNewsAndInterests"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SOFTWARE\Policies\Microsoft\Windows\System"" /v ""EnableActivityFeed"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKCU\Control Panel\International\User Profile"" /v ""HttpAcceptLanguageOptOut"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKCU\Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo"" /v ""Enabled"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\Software\Policies\Microsoft\Windows\System"" /v ""EnableActivityFeed"" /t REG_DWORD /d ""0"" /f

7. Disable Popups, Baloon Tips etc.
Reg.exe add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced"" /v ""DisallowShaking"" /t REG_DWORD /d ""1"" /f
Reg.exe add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced"" /v ""EnableBalloonTips"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced"" /v ""ShowSyncProviderNotifications"" /t REG_DWORD /d ""0"" /f
Reg.exe add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userNotificationListener"" /v ""Value"" /t REG_SZ /d ""Deny"" /f
Reg.exe add ""HKLM\Software\Policies\Microsoft\Windows\AdvertisingInfo"" /v ""DisabledByGroupPolicy"" /t REG_DWORD /d ""1"" /f

8. Disabling Windows Insider Experiments
Reg.exe add ""HKLM\SOFTWARE\Microsoft\PolicyManager\current\device\System"" /v ""AllowExperimentation"" /t REG_DWORD /d ""0"" /f 
Reg.exe add ""HKLM\SOFTWARE\Microsoft\PolicyManager\default\System\AllowExperimentation"" /v ""value"" /t REG_DWORD /d ""0"" /f 

9. Disable Unnecessary Apps Run In The Background
reg add ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications"" /v ""GlobalUserDisabled"" /t REG_DWORD /d 00000001 /f";

            ExecuteBatchCommands(batchCommands);
        }

        private void RevertDisableUselessFeatures()
        {
            string batchCommands = @":: 1. Disable Web in Search
reg delete ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""ConnectedSearchUseWeb"" /f
reg delete ""HKLM\Software\Policies\Microsoft\Windows\Windows Search"" /v ""DisableWebSearch"" /f
reg delete ""HKLM\Software\Microsoft\Windows\CurrentVersion\Search"" /v ""BingSearchEnabled"" /f

:: 2. Disable Transparency
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize"" /v ""EnableTransparency"" /f

:: 3. Disable Ease of Access Tab
reg delete ""HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys"" /v ""Flags"" /f
reg delete ""HKEY_CURRENT_USER\Control Panel\Accessibility\StickyKeys"" /v ""Flags"" /f
reg delete ""HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response"" /v ""Flags"" /f
reg delete ""HKEY_CURRENT_USER\Control Panel\Accessibility\ToggleKeys"" /v ""Flags"" /f

:: 4. Disable Background Apps in Search
reg delete ""HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search"" /v ""BackgroundAppGlobalToggle"" /f

:: Delete unnecessary permissions for certain capabilities
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\activity"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appDiagnostics"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\appointments"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\bluetoothSync"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\broadFileSystemAccess"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\cellularData"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\cellularData\Microsoft.Win32WebViewHost_cw5n1h2txyewy"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\chat"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\contacts"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\documentsLibrary"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\email"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\gazeInput"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location\Microsoft.Win32WebViewHost_cw5n1h2txyewy"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\phoneCall"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\phoneCallHistory"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\picturesLibrary"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\radios"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userAccountInformation"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userAccountInformation\Microsoft.AccountsControl_cw5n1h2txyewy"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userDataTasks"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\userNotificationListener"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\videosLibrary"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam"" /f
reg delete ""HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\Microsoft.Win32WebViewHost_cw5n1h2txyewy"" /f

:: 5. Disable Customer Experience Improvement Program
schtasks /delete /tn ""\Microsoft\Windows\Customer Experience Improvement Program\Consolidator"" /f
schtasks /delete /tn ""\Microsoft\Windows\Customer Experience Improvement Program\BthSQM"" /f
schtasks /delete /tn ""\Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask"" /f
schtasks /delete /tn ""\Microsoft\Windows\Customer Experience Improvement Program\UsbCeip"" /f
schtasks /delete /tn ""\Microsoft\Windows\Customer Experience Improvement Program\Uploader"" /f
schtasks /delete /tn ""\Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser"" /f
schtasks /delete /tn ""\Microsoft\Windows\Application Experience\ProgramDataUpdater"" /f
schtasks /delete /tn ""\Microsoft\Windows\Application Experience\StartupAppTask"" /f
schtasks /delete /tn ""\Microsoft\Windows\Shell\FamilySafetyMonitor"" /f
schtasks /delete /tn ""\Microsoft\Windows\Shell\FamilySafetyRefresh"" /f
schtasks /delete /tn ""\Microsoft\Windows\Shell\FamilySafetyUpload"" /f
schtasks /delete /tn ""\Microsoft\Windows\Maintenance\WinSAT"" /f

:: 6. Disable Activity Feed
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Feeds"" /v ""EnableFeeds"" /f

:: 7. Remove Notifications & Disable Tips
reg delete ""HKCU\Software\Microsoft\Windows\CurrentVersion\PushNotifications"" /v ""ToastEnabled"" /f
reg delete ""HKCU\Software\Microsoft\Windows\CurrentVersion\Notifications"" /v ""ToastEnabled"" /f
reg delete ""HKCU\Software\Microsoft\Windows\CurrentVersion\Notifications"" /v ""PublicKey"" /f
reg delete ""HKCU\Software\Microsoft\Windows\CurrentVersion\Tips"" /v ""ShowTips"" /f

:: 8. Disable Storage Sense
reg delete ""HKCU\Software\Microsoft\Windows\CurrentVersion\StorageSense\Parameters\StoragePolicy"" /v ""Enable"" /f

:: 9. Disable Automatic Updates
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate"" /v ""DisableOSUpgrade"" /f
reg delete ""HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate"" /v ""NoAutoRebootWithLoggedOnUsers"" /f
";

            ExecuteBatchCommands(batchCommands);
        }

        private void ImportPowerPlan()
        {
            string batchCommands = @"powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61 44444244-4444-4444-4444-444444444420
powercfg /changename 44444244-4444-4444-4444-444444444420 ""ToX Free PowerPlan V2.0"" ""Designed To Improve Performance, Latency and Overall Responsivness.""
powercfg -SETACTIVE 44444244-4444-4444-4444-444444444420
powercfg /SETACVALUEINDEX SCHEME_CURRENT SUB_SLEEP AWAYMODE 0
powercfg /SETACVALUEINDEX SCHEME_CURRENT SUB_SLEEP STANDBYIDLE 0
powercfg /SETACVALUEINDEX SCHEME_CURRENT SUB_SLEEP HYBRIDSLEEP 0
powercfg /SETACVALUEINDEX SCHEME_CURRENT SUB_PROCESSOR PROCTHROTTLEMIN 100
powercfg /SETACVALUEINDEX SCHEME_CURRENT SUB_PROCESSOR PROCTHROTTLEMAX 100
powercfg /SETACVALUEINDEX SCHEME_CURRENT SUB_PROCESSOR CPMINCORES 100
# disable hibernate
powercfg /hibernate off
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""HibernateEnabled"" /t REG_DWORD /d ""0"" /f >nul 2>&1""
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power"" /v ""HibernateEnabledDefault"" /t REG_DWORD /d ""0"" /f >nul 2>&1""
# disable lock
Reg.exe add ""HKLM\Software\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings"" /v ""ShowLockOption"" /t REG_DWORD /d ""0"" /f >nul 2>&1""
# disable sleep
Reg.exe add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings"" /v ""ShowSleepOption"" /t REG_DWORD /d ""0"" /f >nul 2>&1""
# disable fast boot
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Power"" /v ""HiberbootEnabled"" /t REG_DWORD /d ""0"" /f >nul 2>&1""
# unpark cpu cores
Reg.exe add ""HKLM\SYSTEM\ControlSet001\Control\Power\PowerSettings\54533251-82be-4824-96c1-47b60b740d00\0cc5b647-c1df-4637-891a-dec35c318583"" /v ""ValueMax"" /t REG_DWORD /d ""0"" /f >nul 2>&1""
# disable power throttling
Reg.exe add ""HKLM\SYSTEM\CurrentControlSet\Control\Power\PowerThrottling"" /v ""PowerThrottlingOff"" /t REG_DWORD /d ""1"" /f >nul 2>&1""
# unhide hub selective suspend timeout
Reg.exe add ""HKLM\System\ControlSet001\Control\Power\PowerSettings\2a737441-1930-4402-8d77-b2bebba308a3\0853a681-27c8-4100-a2fd-82013e970683"" /v ""Attributes"" /t REG_DWORD /d ""2"" /f >nul 2>&1""
# unhide usb 3 link power management
Reg.exe add ""HKLM\System\ControlSet001\Control\Power\PowerSettings\2a737441-1930-4402-8d77-b2bebba308a3\d4e98f31-5ffe-4ce1-be31-1b38b384c009"" /v ""Attributes"" /t REG_DWORD /d ""2"" /f >nul 2>&1""
# MODIFY DESKTOP & LAPTOP SETTINGS
# hard disk turn off hard disk after 0%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 0012ee47-9041-4b5d-9b77-535fba8b1442 6738e2c4-e8a5-4a42-b16a-e040e769756e 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 0012ee47-9041-4b5d-9b77-535fba8b1442 6738e2c4-e8a5-4a42-b16a-e040e769756e 0x00000000
# desktop background settings slide show paused
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 0d7dbae2-4294-402a-ba8e-26777e8488cd 309dce9b-bef4-4119-9921-a851fb12f0f4 001
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 0d7dbae2-4294-402a-ba8e-26777e8488cd 309dce9b-bef4-4119-9921-a851fb12f0f4 001
# wireless adapter settings power saving mode maximum performance
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 19cbb8fa-5279-450e-9fac-8a3d5fedd0c1 12bbebe6-58d6-4636-95bb-3217ef867c1a 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 19cbb8fa-5279-450e-9fac-8a3d5fedd0c1 12bbebe6-58d6-4636-95bb-3217ef867c1a 000
# sleep
# sleep after 0%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 29f6c1db-86da-48c5-9fdb-f2b67b1f44da 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 29f6c1db-86da-48c5-9fdb-f2b67b1f44da 0x00000000
# allow hybrid sleep off
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 94ac6d29-73ce-41a6-809f-6363ba21b47e 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 94ac6d29-73ce-41a6-809f-6363ba21b47e 000
# hibernate after
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 9d7815a6-7ee4-497e-8888-515a05f02364 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 9d7815a6-7ee4-497e-8888-515a05f02364 0x00000000
# allow wake timers disable
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 bd3b718a-0680-4d9d-8ab2-e1d2b4ac806d 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 238c9fa8-0aad-41ed-83f4-97be242c8f20 bd3b718a-0680-4d9d-8ab2-e1d2b4ac806d 000
# usb settings
# hub selective suspend timeout 0
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 2a737441-1930-4402-8d77-b2bebba308a3 0853a681-27c8-4100-a2fd-82013e970683 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 2a737441-1930-4402-8d77-b2bebba308a3 0853a681-27c8-4100-a2fd-82013e970683 0x00000000
# usb selective suspend setting disabled
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 000
# usb 3 link power management - off
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 2a737441-1930-4402-8d77-b2bebba308a3 d4e98f31-5ffe-4ce1-be31-1b38b384c009 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 2a737441-1930-4402-8d77-b2bebba308a3 d4e98f31-5ffe-4ce1-be31-1b38b384c009 000
# power buttons and lid start menu power button shut down
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 4f971e89-eebd-4455-a8de-9e59040e7347 a7066653-8d6c-40a8-910e-a1f54b84c7e5 002
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 4f971e89-eebd-4455-a8de-9e59040e7347 a7066653-8d6c-40a8-910e-a1f54b84c7e5 002
# pci express link state power management off
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 501a4d13-42af-4429-9fd1-a8218c268e20 ee12f906-d277-404b-b6da-e5fa1a576df5 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 501a4d13-42af-4429-9fd1-a8218c268e20 ee12f906-d277-404b-b6da-e5fa1a576df5 000
# processor power management
# minimum processor state 100%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 54533251-82be-4824-96c1-47b60b740d00 893dee8e-2bef-41e0-89c6-b55d0929964c 0x00000064
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 54533251-82be-4824-96c1-47b60b740d00 893dee8e-2bef-41e0-89c6-b55d0929964c 0x00000064
# system cooling policy active
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 54533251-82be-4824-96c1-47b60b740d00 94d3a615-a899-4ac5-ae2b-e4d8f634367f 001
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 54533251-82be-4824-96c1-47b60b740d00 94d3a615-a899-4ac5-ae2b-e4d8f634367f 001
# maximum processor state 100%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 54533251-82be-4824-96c1-47b60b740d00 bc5038f7-23e0-4960-96da-33abaf5935ec 0x00000064
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 54533251-82be-4824-96c1-47b60b740d00 bc5038f7-23e0-4960-96da-33abaf5935ec 0x00000064
# display
# turn off display after 0%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e 0x00000000
# display brightness 100%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 aded5e82-b909-4619-9949-f5d71dac0bcb 0x00000064
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 aded5e82-b909-4619-9949-f5d71dac0bcb 0x00000064
# dimmed display brightness 100%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 f1fbfde2-a960-4165-9f88-50667911ce96 0x00000064
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 f1fbfde2-a960-4165-9f88-50667911ce96 0x00000064
# enable adaptive brightness off
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 fbd9aa66-9553-4097-ba44-ed6e9d65eab8 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 7516b95f-f776-4464-8c53-06167f40cc99 fbd9aa66-9553-4097-ba44-ed6e9d65eab8 000
# video playback quality bias video playback performance bias
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 9596fb26-9850-41fd-ac3e-f7c3c00afd4b 10778347-1370-4ee0-8bbd-33bdacaade49 001
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 9596fb26-9850-41fd-ac3e-f7c3c00afd4b 10778347-1370-4ee0-8bbd-33bdacaade49 001
# when playing video optimize video quality
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 9596fb26-9850-41fd-ac3e-f7c3c00afd4b 34c7b99f-9a6d-4b3c-8dc7-b6693b78cef4 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 9596fb26-9850-41fd-ac3e-f7c3c00afd4b 34c7b99f-9a6d-4b3c-8dc7-b6693b78cef4 000
# MODIFY LAPTOP SETTINGS
# intel(r) graphics settings intel(r) graphics power plan maximum performance
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 44f3beca-a7c0-460e-9df2-bb8b99e0cba6 3619c3f2-afb2-4afc-b0e9-e7fef372de36 002
Clear-Host
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 44f3beca-a7c0-460e-9df2-bb8b99e0cba6 3619c3f2-afb2-4afc-b0e9-e7fef372de36 002
Clear-Host
# amd power slider overlay best performance
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 c763b4ec-0e50-4b6b-9bed-2b92a6ee884e 7ec1751b-60ed-4588-afb5-9819d3d77d90 003
Clear-Host
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 c763b4ec-0e50-4b6b-9bed-2b92a6ee884e 7ec1751b-60ed-4588-afb5-9819d3d77d90 003
Clear-Host
# ati graphics power settings ati powerplay settings maximize performance
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 f693fb01-e858-4f00-b20f-f30e12ac06d6 191f65b5-d45c-4a4f-8aae-1ab8bfd980e6 001
Clear-Host
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 f693fb01-e858-4f00-b20f-f30e12ac06d6 191f65b5-d45c-4a4f-8aae-1ab8bfd980e6 001
Clear-Host
# switchable dynamic graphics global settings maximize performance
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e276e160-7cb0-43c6-b20b-73f5dce39954 a1662ab2-9d34-4e53-ba8b-2639b9e20857 003
Clear-Host
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e276e160-7cb0-43c6-b20b-73f5dce39954 a1662ab2-9d34-4e53-ba8b-2639b9e20857 003
Clear-Host
# battery
# critical battery notification off
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 5dbb7c9f-38e9-40d2-9749-4f8a0e9f640f 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 5dbb7c9f-38e9-40d2-9749-4f8a0e9f640f 000
# critical battery action do nothing
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 637ea02f-bbcb-4015-8e2c-a1c7b9c0b546 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 637ea02f-bbcb-4015-8e2c-a1c7b9c0b546 000
# low battery level 0%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 8183ba9a-e910-48da-8769-14ae6dc1170a 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 8183ba9a-e910-48da-8769-14ae6dc1170a 0x00000000
# critical battery level 0%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 9a66d8d7-4ff7-4ef9-b5a2-5a326ca2a469 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f 9a66d8d7-4ff7-4ef9-b5a2-5a326ca2a469 0x00000000
# low battery notification off
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f bcded951-187b-4d05-bccc-f7e51960c258 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f bcded951-187b-4d05-bccc-f7e51960c258 000
# low battery action do nothing
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f d8742dcb-3e6a-4b3c-b3fe-374623cdcf06 000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f d8742dcb-3e6a-4b3c-b3fe-374623cdcf06 000
# reserve battery level 0%
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f f3c5027d-cd16-4930-aa6b-90db844a8f00 0x00000000
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 e73a048d-bf27-4f12-9731-8b2076e8891f f3c5027d-cd16-4930-aa6b-90db844a8f00 0x00000000
# immersive control panel
# low screen brightness when using battery saver disable
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 de830923-a562-41af-a086-e3a2c6bad2da 13d09884-f74e-474a-a852-b6bde8ad03a8 0x00000064
Clear-Host
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 de830923-a562-41af-a086-e3a2c6bad2da 13d09884-f74e-474a-a852-b6bde8ad03a8 0x00000064
Clear-Host
# immersive control panel
# turn battery saver on automatically at never
powercfg /setacvalueindex 44444244-4444-4444-4444-444444444420 de830923-a562-41af-a086-e3a2c6bad2da e69653ca-cf7f-4f05-aa73-cb833fa90ad4 0x00000000
Clear-Host
powercfg /setdcvalueindex 44444244-4444-4444-4444-444444444420 de830923-a562-41af-a086-e3a2c6bad2da e69653ca-cf7f-4f05-aa73-cb833fa90ad4 0x00000000
";

            ExecuteBatchCommands(batchCommands);
        }

        private void RevertImportPowerPlan()
        {
            string batchCommands = @"powercfg -delete 44444244-4444-4444-4444-444444444420";

            ExecuteBatchCommands(batchCommands);
        }

        private void DisableCortana()
        {
            string batchCommands = @"powershell Get-AppxPackage -Name Microsoft.549981C3F5F10 | Remove-AppxPackage";

            ExecuteBatchCommands(batchCommands);
        }

        private void RevertDisableCortana()
        {
            string batchCommands = @"powershell Add-AppxPackage -register ""C:\Program Files\WindowsApps\Microsoft.549981C3F5F10_*\CortanaApp.exe"" -DisableDevelopmentMode";

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
        #region NextPage
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            if (Main.Instance != null)
            {
                // Create an instance of the WindowsTweaksP2 form
                WindowsTweaksP2 myForm = new WindowsTweaksP2();
                myForm.TopLevel = false; // Set to false since it's being shown in a panel

                // Clear existing controls in the MainPanel
                Main.Instance.MainPanel.Controls.Clear();

                // Fade out the current content in the panel
                Main.Instance.guna2Transition1.HideSync(Main.Instance.MainPanel);

                // Show the new form and apply the transition after a short delay
                Task.Delay(1).ContinueWith(_ =>
                {
                    Main.Instance.MainPanel.Invoke((Action)(() =>
                    {
                        Main.Instance.MainPanel.Controls.Add(myForm); // Add the new form to the panel
                        myForm.Show(); // Show the new form
                        Main.Instance.guna2Transition1.ShowSync(Main.Instance.MainPanel); // Fade in the new content
                    }));
                });
            }
        }
        #endregion

    }
}