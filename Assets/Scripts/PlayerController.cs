using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.5f;
    private Rigidbody2D rigidBody2D;
    public Animator animator;
    private PlayerData playerData, playerDataNra;

    void Start()
    {
         rigidBody2D = GetComponent<Rigidbody2D>();
         playerData = new PlayerData();
         //playerData = GetComponent<PlayerData>();
         playerData.plummie_tag = "juju carente";
         //StartCoroutine(Upload(playerData.Stringfy()));
         StartCoroutine(Download("nraboy", result => {
            Debug.Log("res = ");
            Debug.Log(result);
            playerDataNra = result; 
            Debug.Log("download =" + playerDataNra.Stringfy());
         }));
         
         StartCoroutine(Upload(playerData.Stringfy(), result => {
            Debug.Log(result);
         }));
    }
    void Update()
    {
    }
    void FixedUpdate() {

        //force applied over the rigidbody - sliding on ice effect
        /*float h = 0.0f;
        float v = 0.0f;
        if (Input.GetKey("w")) { v = 1.0f; }
        if (Input.GetKey("s")) { v = -1.0f; }
        if (Input.GetKey("a")) { h = -1.0f; }
        if (Input.GetKey("d")) { h = 1.0f; }
        rigidBody2D.AddForce(new Vector2(h, v) * speed);*/

        /*
        float h = 0.0f;
        float v = 0.0f;
        if (Input.GetKey("w")) { v = 1.0f; }
        if (Input.GetKey("s")) { v = -1.0f; }
        if (Input.GetKey("a")) { h = -1.0f; }
        if (Input.GetKey("d")) { h = 1.0f; }
        rigidBody2D.MovePosition(rigidBody2D.position + (new Vector2(h, v) * speed * Time.fixedDeltaTime));
        */
        //alternate movement programming, equals above but also concede arrow keys usage
    
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        animator.SetFloat("h", h);
        animator.SetFloat("v", v);
        rigidBody2D.MovePosition(rigidBody2D.position + (new Vector2(h, v) * speed * Time.fixedDeltaTime));

        if(rigidBody2D.position.x > 7.0f) {
            Debug.Log("Uploading...");
            StartCoroutine(Upload(playerData.Stringfy(), result => {
                Debug.Log(result);
            }));
        }

    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        playerData.collisions++;
    }

    IEnumerator Download(string id, System.Action<PlayerData> callback = null){
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/plummies/" + id))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError  || request.result == UnityWebRequest.Result.ProtocolError){
                Debug.Log(request.error);
                if (callback != null){
                    callback.Invoke(null);
                }
            }
            else {
                if (callback != null){
                    callback.Invoke(PlayerData.parse(request.downloadHandler.text));
                }
            }
        }
    }
    IEnumerator Upload(string profile, System.Action<bool> callback = null){
        using(UnityWebRequest request = new UnityWebRequest("http://localhost:3000/plummies", "POST")){
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(profile);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            
            if(request.result == UnityWebRequest.Result.ConnectionError  || request.result == UnityWebRequest.Result.ProtocolError){
                Debug.Log(request.error);
                if(callback != null){
                    callback.Invoke(false);
                }   
            }else{
                Debug.Log(request.downloadHandler.text);
                if(callback != null){
                callback.Invoke(request.downloadHandler.text != "{}");
                }
            }

        }
    }

}
