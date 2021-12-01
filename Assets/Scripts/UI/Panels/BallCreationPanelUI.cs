using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BallCreationPanelUI : MonoBehaviour
{

    [Header("Images")]
    [SerializeField] Image _ballImage;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] TextMeshProUGUI _sizeText;

    float _ballSpeed;
    float _ballSize;
    string _ballName;


    public void SetGreenValue(float value) => _ballImage.color = new Color(_ballImage.color.r, value , _ballImage.color.b, 1);
    public void SetRedValue(float value) => _ballImage.color = new Color(value, _ballImage.color.g, _ballImage.color.b, 1);
    public void SetBlueValue(float value) => _ballImage.color = new Color(_ballImage.color.r, _ballImage.color.g, value, 1);

    public void SetSpeedValue(float value)
    {
        var roundedValue = RoundToDecimals(value);
        _ballSpeed = roundedValue;
        _speedText.text = roundedValue.ToString();
    }

    public void SetSizeValue(float value)
    {
        var roundedValue = RoundToDecimals(value);
        _ballSize = roundedValue;
        _sizeText.text = roundedValue.ToString();
    }

    public void SetName(string name) => _ballName = name;
    public void CreateNewBall() => BallCreator.CreateBallData(_ballName, _ballSpeed, _ballSize, _ballImage.color);

    float RoundToDecimals(float value)
    {
        double d = System.Math.Round(value, 2);
        return (float)d;
    }
}
