using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


/*------------------------------------------------------------------------------ 
 * ##############################################################################
 * 
 * This code provides API access to the awesome NFTMakerPro API developed by Patrick Tobler @padierfind.
 * 
 * Code written by Andrew King @cryptoknitties.
 * Licensed under CC BY 4.0 
 * 
 * Recommended usage is as follows:
 * You can add specific project and NFT details in individual script instances or you can change the fields on a central 'NFT Manager' instance with the incoming methods in section 3 (recommended).
 * 
 */

/*------------------------------------------------------------------------------
 *##############################################################################
 * 
 * Code sections
 * 
 * Section 01 - I/O, classes and setup.
 * Section 02 - Incoming method calls for API
 * Section 03 - Incoming method calls for changes to the request data
 * Section 04 - API Web Requests
 * Section 05 - Data processing and serialisation
 * Section 06 - UI methods
 * 
 */
public class NFTMakerProAPI : MonoBehaviour
{

    /*------------------------------------------------------------------------------
      *##############################################################################
      * 
      * Section 01
      * I/O, classes and setup.
      * 
      */


    // public SpecificNFT class - return data classes are avaliable from NFTMakerPro.io
    public class SpecificNFT
    {
        public string paymentAddress;
        public string expires;
        public string adaToSend;        
        
    }

    // Project API information
    [Header("NFTMakerPro API info")]
    public string APIHTTPAddress;
    public string APIProjectKey;
    public string APIProjectID;
    // Specific Request information - you may want to change this information for each API call - I have provided methods for this.
    [Header("Specific request details")]
    public string APINFTID;
    public int APINFTQuantity;
    public int APINFTLovelace;
    // UI output objects - change these to what ever you want - there are methods for this.
    public Text UIADDRDisplay;
    public Text UIExpiresDisplay;
    public Text UIADADisplay;

    /*------------------------------------------------------------------------------
    *##############################################################################
    * 
    * Section 02
    * Incoming method calls for API
    * 
    */

    public void GetADDRSpecNFT()
    {
        string uri = APIHTTPAddress + "GetAddressForSpecificNftSale" + "/" + APIProjectKey + "/" + APIProjectID + "/" + APINFTID + "/" + APINFTQuantity + "/" + APINFTLovelace;
        StartCoroutine(APIGetADDRSpecNFT(uri));
        Debug.Log(uri);
    }

    public void GetAddressForRandomNftSale()
    {
        string uri = APIHTTPAddress + "GetAddressForRandomNftSale" + "/" + APIProjectKey + "/" + APIProjectID + "/" + APINFTQuantity + "/" + APINFTLovelace;
        StartCoroutine(APIGetADDRRandomNFT(uri));
    }









    /*------------------------------------------------------------------------------
    *##############################################################################
    * 
    * Section 03
    * Incoming method calls for changes to the public API info and request details
    * 
    */

    public void changeAPINFTID(string newNFTID)
    {
        APINFTID = newNFTID;
    }
    public void changeAPINFTQuantity(int newNFTQuantity)
    {
        APINFTQuantity = newNFTQuantity;
    }
    public void changePolicyID(int newNFTLovelace)
    {
        APINFTLovelace = newNFTLovelace;
    }

    /*------------------------------------------------------------------------------
     *##############################################################################
     * 
     * Section 04
     * API Web Requests
     * 
     */

    IEnumerator APIGetADDRSpecNFT(string uri)
    {
        Debug.Log("Getting Data from API using: " + uri);
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Data Retrieved from API");
            Debug.Log(www.downloadHandler.text);
            processSpecificNFTData(www.downloadHandler.text);
        }
    }


    IEnumerator APIGetADDRRandomNFT(string uri)
    {
        Debug.Log("Getting Data from API using: " + uri);
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Data Retrieved from API");
            Debug.Log(www.downloadHandler.text);
            processSpecificNFTData(www.downloadHandler.text);
        }
    }

    /*------------------------------------------------------------------------------
    *##############################################################################
    * 
    * Section 05
    * Data processing and serialisation
    * 
    */

    public void processSpecificNFTData(string JSONText)
    {
        SpecificNFT myNFT = new SpecificNFT();
        myNFT = JsonConvert.DeserializeObject<SpecificNFT>(JSONText);
        Debug.Log("ADDR :" + myNFT.paymentAddress);
        Debug.Log("expires :" + myNFT.expires);
        Debug.Log("ADA to send :" + myNFT.adaToSend);
        updateUI(myNFT.paymentAddress, myNFT.expires, myNFT.adaToSend);
    }

    /*------------------------------------------------------------------------------
    *##############################################################################
    * 
    * Section 06
    * UI functions
    * 
    */

    public void updateUI(string paymentAddress, string expires, string adaToSend)
    {
        if (UIADDRDisplay != null)
        {
            UIADDRDisplay.text = paymentAddress;
        }
        if (UIExpiresDisplay != null)
        {
            UIExpiresDisplay.text = expires;
        }
        if (UIADADisplay != null)
        {
            UIADADisplay.text = adaToSend;
        }
    }

}
