//Сохранение настроек в PlayerPrefs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Тип параметров с цифровым значением
/// </summary>
public enum Type_parameter_value
{
    Sound_music,
    Sound_effect,
    Language,
}

/// <summary>
/// Тип параметров с bool значением
/// </summary>
public enum Type_parameter_bool
{
    Alert_bool,
    Vibration_bool
}

public static class Setting_PlayerPrefs
{

    /// <summary>
    /// Сохранить параметр с цифровым значением
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <param name="_value">Значение</param>
    public static void Save_parameter_value(Type_parameter_value _parameter, float _value)
    {
        switch (_parameter)
        {
            case Type_parameter_value.Sound_effect:
                PlayerPrefs.SetFloat("Effect_sound_value", _value);
                break;

            case Type_parameter_value.Sound_music:
                PlayerPrefs.SetFloat("Music_value", _value);
                break;

            case Type_parameter_value.Language:
                PlayerPrefs.SetInt("Language_option", (int)_value);
                break;
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Сохранить параметр c bool значением
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <param name="_bool">Значение да или нет</param>
    public static void Save_parameter_bool(Type_parameter_bool _parameter, bool _bool)
    {
        int value = 0;

        value = _bool ? 1 : 0;

        switch (_parameter)
        {

            case Type_parameter_bool.Alert_bool:
                PlayerPrefs.SetInt("Alert_bool", value);
                break;

            case Type_parameter_bool.Vibration_bool:
                PlayerPrefs.SetInt("Vibration_bool", value);
                break;
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Узнать значение параметра
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <returns></returns>
    public static float Know_parameter_value(Type_parameter_value _parameter)
    {
        float value = 0;

        switch (_parameter)
        {
            case Type_parameter_value.Sound_effect:
                if (PlayerPrefs.HasKey("Effect_sound_value"))
                    value = PlayerPrefs.GetFloat("Effect_sound_value");
                else
                    value = 1;
                break;

            case Type_parameter_value.Sound_music:
                if (PlayerPrefs.HasKey("Music_value"))
                    value = PlayerPrefs.GetFloat("Music_value");
                else
                    value = 1;
                break;

            case Type_parameter_value.Language:
                if (PlayerPrefs.HasKey("Language_option"))
                    value = PlayerPrefs.GetInt("Language_option");
                else
                    value = 0;
                break;
        }

        return value;
    }

    /// <summary>
    /// Узнать активность параметра
    /// </summary>
    /// <param name="_parameter">Тип параметра</param>
    /// <returns></returns>
    public static bool Know_parameter_bool (Type_parameter_bool _parameter)
    {
        bool bool_ = true;

        int value = 0;

        switch (_parameter)
        {
            case Type_parameter_bool.Alert_bool:
                if (PlayerPrefs.HasKey("Alert_bool"))
                {
                    value = PlayerPrefs.GetInt("Alert_bool");
                    bool_ = value == 1 ? true : false;
                }
                else
                    bool_ = true;
                break;

            case Type_parameter_bool.Vibration_bool:
                if (PlayerPrefs.HasKey("Vibration_bool"))
                {
                    value = PlayerPrefs.GetInt("Vibration_bool");
                    bool_ = value == 1 ? true : false;
                }
                else
                    bool_ = true;
                break;
        }

        return bool_;
    }

}
