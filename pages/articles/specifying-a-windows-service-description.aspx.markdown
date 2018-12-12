---
title: "Specifying a Windows Service Description"
permalink: /articles/specifying-a-windows-service-description.aspx
---

Source listing for methods to add and remove a Windows Service
description to and from the Registry.

AddServiceDescriptionToRegistry should be called within the Install
method of a System.Configuration.Install.Installer instance while
RemoveServiceDescriptionFromRegistry should be called from the Uninstall
method.

Notice that these methods use a Log

```csharp
/// <summary>
/// Adds the service description to the registry.
/// </summary>
/// <param name="serviceName"></param>
/// <param name="description"></param>
protected virtual void AddServiceDescriptionToRegistry(string serviceName, string description)
{
    Microsoft.Win32.RegistryKey system;
    Microsoft.Win32.RegistryKey    currentControlSet; //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
    Microsoft.Win32.RegistryKey services; //...\Services
    Microsoft.Win32.RegistryKey service; //...\<Service Name>
    Microsoft.Win32.RegistryKey config; //...\Parameters - this is where you can put service-specific configuration
    try
    {
        //Open the HKEY_LOCAL_MACHINE\SYSTEM key
        system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
        //Open CurrentControlSet
        currentControlSet = system.OpenSubKey("CurrentControlSet");
        //Go to the services key
        services = currentControlSet.OpenSubKey("Services");
        //Open the key for your service, and allow writing
        service = services.OpenSubKey(serviceName, true);
        //Add your service's description as a REG_SZ value named "Description"
        service.SetValue("Description", description);
        //(Optional) Add some custom information your service will use...
        config = service.CreateSubKey("Parameters");
    }
    catch(Exception e)
    {
        //Log.Error("Error occurred while attempting to add a service description to the registry.", e);
    }
}
/// <summary>
/// Removes the service description from the registry.
/// </summary>
/// <param name="serviceName"></param>
protected virtual void RemoveServiceDescriptionFromRegistry(string serviceName)
{
    Microsoft.Win32.RegistryKey system;
    Microsoft.Win32.RegistryKey    currentControlSet; //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
    Microsoft.Win32.RegistryKey services; //...\Services
    Microsoft.Win32.RegistryKey service; //...\<Service Name>
    Microsoft.Win32.RegistryKey config; //...\Parameters - this is where you can put service-specific configuration
    try
    {
        //Drill down to the service key and open it with write permission
        system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
        currentControlSet = system.OpenSubKey("CurrentControlSet");
        services = currentControlSet.OpenSubKey("Services");
        service = services.OpenSubKey(serviceName, true);
        //Delete any keys you created during installation (or that your service created)
        service.DeleteSubKeyTree("Parameters");
        //...
    }
    catch(Exception e)
    {
        //Log.Error("Error occurred while trying to remove the service description from the registry.", e);
    }
}
```

