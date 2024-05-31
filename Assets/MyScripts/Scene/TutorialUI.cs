using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : RPGMonoBehaviour
{
    public GameObject tutorialUI;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI indexText;

    public string[] titleTexts;
    [TextArea(5,10)]
    public string[] mainTexts;
    public int index;
    #region SetText
    public void SetText()
    {
        titleTexts[0] = "Điều Khiển";
        titleTexts[1] = "Tương tác";
        titleTexts[2] = "Chiến đấu";
        titleTexts[3] = "Gán phím tắt";
        titleTexts[4] = "Cấp độ và trang bị";
        titleTexts[5] = "Khám phá thế giới";
        titleTexts[6] = "Quái vật";
        titleTexts[7] = "Cài đặt và lưu game";
        titleTexts[8] = "Phím tắt trong game";
        mainTexts[0] = "- Sử dụng các phím W,S,A,D để di chuyển lên, xuống trái phải. " +
                        "\n- Sử dụng chuột trái để tấn công cận chiến./nGiữ chuột phải và di chuyển để xoay camera." +
                        "\n- Sử dụng phím cách để nhảy.";
        mainTexts[1] = "- Nói chuyện với NPC để nhận nhiệm vụ hay mua vật phẩm trong game.";
        mainTexts[2] = "- Sử dụng chuột trái để tấn công." +
                        "\n- Gán kỹ năng vào các ô phím tắt Q, E, Z, C để sử dụng kỹ năng." +
                        "\n- Gán các vật phẩm vào ô phím tắt 1, 2, 3, 4 để sử dụng vật phẩm.";
        mainTexts[3] = "Vật phẩm :" +
                        "\n- Kéo và nhấn phím tắt 1 ,2, 3 hay 4 cùng lúc để gán." +
                        "\n- Nhấn chuột vào phím tắt để hủy gán và trả vật phẩm về túi." +
                        "\nKỹ năng :" +
                        " \n- Nhấn nút gán và phím tắt Q, E, Z, C  để gán." +
                        "\n- Nhấn chuột vào phím tắt để hủy gán.";
        mainTexts[4] = "Khi lên cấp người chơi sẽ được tăng các chỉ số : +10 máu, +10 mana,+10 stamina, + 0,1 giáp,+ 1 công." +
                        "\nTrang bị chủ yếu gồm: " +
                        "\n- Vũ khí : rìu, thương, kiếm." +
                        "\n- Giáp : Găng tay, mũ, giáp.";
        mainTexts[5] = "Hiện tại có 2 map chính :" +
                        "\n- Map làng mạc ." +
                        "\n-Map rừng rậm(Được mở sau khi hoàn thành nhiệm vụ ở Map 1).";
        mainTexts[6] = "Quái vật là kẻ thù chính trong game." +
                        "\n- Quái thường : Mỗi loại quái khác nhau thì có chỉ số khác nhau." +
                        "\n- Quái Boss : là những con quái vật mạnh mẽ và có sức mạnh khủng khiếp và những kỹ năng.";
        mainTexts[7] = "- Mở cài đặt bằng cách nhấn chuột vào biểu tượng cài đặt." +
                        "\n- Gồm các lựa chọn như thoát, menu chính, điều chỉnh âm thanh, lưu trò chơi.";
        mainTexts[8] = "- T : hiển thị thông tin nhân vật." +
                        "\n- I: Mở túi đồ." +
                        "\n- K: Mở kỹ năng." +
                        "\n- N : Xem nhiệm vụ";
    }
    #endregion
    private void Start()
    {
        titleText.text = titleTexts[0];
        mainText.text = mainTexts[0];
        indexText.text = (index + 1) + "/" + (titleTexts.Length);
    }
    protected override void LoadComponents()
    {
        LoadTutorialUI();
    }
    protected virtual void LoadTutorialUI()
    {
        if (this.tutorialUI != null) return;
        this.tutorialUI = gameObject;
    }
    public virtual void CloseButton()
    {
        tutorialUI.SetActive(false);
    }
    public virtual void NextButton()
    {
        Debug.Log("Click");
        if (index < titleTexts.Length-1)
        {
            index++;
            titleText.text = titleTexts[index];
            mainText.text = mainTexts[index];
        }
        indexText.text = (index+1) + "/" + (titleTexts.Length);
    }
    public virtual void BackButton()
    {
        Debug.Log("Click");
        if(index > 0)
        {
            index--;
            titleText.text = titleTexts[index];
            mainText.text = mainTexts[index];
        }
        indexText.text = (index+1) + "/" + (titleTexts.Length);
    }
}
