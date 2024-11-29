using System.Collections.Generic;
using UnityEngine;

public class WorkSceneBootstrap : MonoBehaviour
{
    public static ServiceLocator ServiceLocator = new ServiceLocator();

    private PinTooltip _pinTooltip;
    private PinInfoExpand _pinInfoExpand;
    private PinInfoEditor _pinInfoEditor;
    private PinScroller _pinInfoScroller;

    private ApplicationDataStorageAgent _applicationDataStorageAgent;
    private ApplicationData _applicationData;

    private Map _map;

    private void Awake()
    {
        ServiceLocator = new ServiceLocator();

        _pinTooltip = FindAnyObjectByType<PinTooltip>(FindObjectsInactive.Include);
        _pinTooltip.Initialize();
        ServiceLocator.Register<PinTooltip>(_pinTooltip);

        _pinInfoExpand = FindAnyObjectByType<PinInfoExpand>(FindObjectsInactive.Include);
        _pinInfoExpand.Initialize();
        ServiceLocator.Register<PinInfoExpand>(_pinInfoExpand);

        //Loading
        _applicationDataStorageAgent = new ApplicationDataStorageAgent();
        _applicationData = _applicationDataStorageAgent.Load();

        _map = FindAnyObjectByType<Map>(FindObjectsInactive.Include);
        List<Pin> pins = _map.Initialize(_applicationData.pinInfos);
        ServiceLocator.Register<Map>(_map);

        _pinInfoScroller = FindAnyObjectByType<PinScroller>(FindObjectsInactive.Include);
        _pinInfoScroller.Initialize(pins);
        ServiceLocator.Register<PinScroller>(_pinInfoScroller);

        _pinInfoEditor = FindAnyObjectByType<PinInfoEditor>(FindObjectsInactive.Include);
        _pinInfoEditor.Initialize();
        ServiceLocator.Register<PinInfoEditor>(_pinInfoEditor);
    }

    private void OnApplicationQuit()
    {
        Pin[] pins = FindObjectsByType<Pin>(FindObjectsSortMode.None);
        _applicationData = new ApplicationData();
        foreach (Pin pin in pins)
        {
            _applicationData.pinInfos.Add(pin.Info);
        }

        _applicationDataStorageAgent.Save(_applicationData);
    }
}
