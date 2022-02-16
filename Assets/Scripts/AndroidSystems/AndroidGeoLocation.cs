using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AndroidGeoLocation : MonoBehaviour
{
    private string _location="not set";
    public AndroidGeoLocation()
    { }
    public string GetGeoLocation()
    {
        Debug.Log(_location);

        StartCoroutine(GeoLocation());
        return _location;
    }

    IEnumerator GeoLocation()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            _location = "Timed out";
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            _location = "Unable to determine device location";
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            _location = Input.location.lastData.latitude + ", " + Input.location.lastData.longitude + ", " + Input.location.lastData.altitude + ", " + Input.location.lastData.horizontalAccuracy + ", " + Input.location.lastData.timestamp;
        }
        Debug.Log(_location);
        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();

        yield return null;
    }
}
