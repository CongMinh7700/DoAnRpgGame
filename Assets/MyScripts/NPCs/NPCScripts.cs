using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScripts : RPGMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected GameObject messageBox;
    [SerializeField] public int shopNumber;
    //Dialogues
    [Header("Talk & Quest")]
    public Quest[] quests;
    public int questIndex;
    public int dialogueIndex ;
    private bool isAnimatingText = false;
    private Coroutine textAnimationCoroutine;
    //Full Text không cho F nữa
    [SerializeField] private bool isFullText = false;
    //Name của npc
    protected override void LoadComponents()
    {
        this.LoadAnimator();
    }
    private void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInParent<Animator>();
        Debug.LogWarning(transform.name + "|LoadNpcAnim|", gameObject);
        
    }
    private void Start()
    {
        messageBox.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        this.messageBox.GetComponent<MessageManager>().numbShop = shopNumber;
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", true);
            messageBox.SetActive(true);
         
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowDialogue();
            messageBox.GetComponent<MessageManager>().firstTask.SetActive(true);
            isFullText = false;
            dialogueIndex = 0;
            messageBox.GetComponent<MessageManager>().HideButton();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Stay", false);
            messageBox.SetActive(false);
            messageBox.GetComponent<MessageManager>().shops[shopNumber].SetActive(false);
            messageBox.GetComponent<MessageManager>().firstTask.SetActive(false);
            messageBox.GetComponent<MessageManager>().questTask.SetActive(false);

          
        }
    }

    private void Update()
    {
        if (messageBox.GetComponent<MessageManager>().questTask.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isAnimatingText && !isFullText)
            {
                ShowDialogue();
            }
        }
    }
    public void ShowDialogue()
    {
        if (this.shopNumber != messageBox.GetComponent<MessageManager>().numbShop) return;
        if (dialogueIndex >= (quests[0].dialogues.Length - 1))
        {
            dialogueIndex = (quests[0].dialogues.Length - 1);
            isFullText = true;
            messageBox.GetComponent<MessageManager>().ShowButton();
            messageBox.GetComponent<MessageManager>().currentQuest = quests[0];
        }
        //messageBox.GetComponent<MessageManager>().dialogeText.text = quests[0].dialogues[dialogueIndex];
        if (textAnimationCoroutine != null)
            StopCoroutine(textAnimationCoroutine);

        string fullText = quests[0].dialogues[dialogueIndex];
        textAnimationCoroutine = StartCoroutine(AnimateText(fullText));
        dialogueIndex++;
    }
    IEnumerator AnimateText(string fullText)
    {
        isAnimatingText = true;
        string displayedText = "";
        for (int i = 0; i <= fullText.Length; i++)
        {
            displayedText = fullText.Substring(0, i);
            messageBox.GetComponent<MessageManager>().dialogeText.text = displayedText;
            yield return new WaitForSeconds(0.03f); // Thay đổi giá trị này nếu bạn muốn thay đổi tốc độ hiển thị văn bản.
        }
        isAnimatingText = false;
    }
}
