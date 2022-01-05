//Скрипт настроек и задаёт положение настроек согласно сохранению
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_menu : Singleton<Setting_menu>
{
    [Header("Настройки эффектов")]
    [Tooltip("Ползунок громкости эффектов")]
    [SerializeField]
    private Slider Effect_sound_slider = null;

    public delegate void Effect_sound_delegate(float _value);//Делегат изменения громкости эффектов
    public Effect_sound_delegate Effect_sound_d;//Экземпляр делегата изменения громкости эффектов

    [Header("Настройки музыки")]
    [Tooltip("Ползунок громкости музыки")]
    [SerializeField]
    private Slider Music_slider = null;

    public delegate void Music_delegate(float _value);//Делегат изменения громкости музыки
    public Music_delegate Music_d;//Экземпляр делегата изменения громкости музыки

    [Header("Настройки оповещения")]
    [Tooltip("Галочка оповещения")]
    [SerializeField]
    private Toggle Alert_toggle = null;

    [Header("Настройки оповещения")]
    [Tooltip("Галочка вибрации")]
    [SerializeField]
    private Toggle Vibration_toggle = null;

    [Header("Выбранный язык")]
    [Tooltip("Список выбора языка")]
    [SerializeField]
    private Dropdown Language_option = null;


    public delegate void Language_delegate(int _index);//Делегат изменения языка
    public Language_delegate Language_d;//Экземпляр делегата изменения языка

    public delegate void Input_key_delegate();//Делегат изменения клавиш управления
    public Input_key_delegate Input_key_d;//Экземпляр делегата изменения клавиш управления

    public delegate void Reset_delegate();//Делегат сбрасывающий насйтроки
    public Reset_delegate Reset_d;//Экземпляр делегата сбрасывающий насйтроки

    private void Start()
    {
        Preparation();
    }

    void Preparation()//Подготовка при включение
    {
            Effect_sound_slider.value = Setting_PlayerPrefs.Know_parameter_value(Type_parameter_value.Sound_effect);

            Music_slider.value = Setting_PlayerPrefs.Know_parameter_value(Type_parameter_value.Sound_music);

            Language_option.value = (int)Setting_PlayerPrefs.Know_parameter_value(Type_parameter_value.Language);

            Alert_toggle.isOn = Setting_PlayerPrefs.Know_parameter_bool(Type_parameter_bool.Alert_bool);

            Vibration_toggle.isOn = Setting_PlayerPrefs.Know_parameter_bool(Type_parameter_bool.Vibration_bool);


    }

    public void Clear_save()//Очистить сохранения
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void Reset_setting()//Вернуть настройки в изначальное состояние
    {
        Sound_effect_control(1);
        Sound_music_control(1);
        Alert_control(true);
        Vibration_control(true);

        Preparation();

        Reset_d?.Invoke();
    }

    public void Input_key_control()//Изменение клавиш управления
    {
        Input_key_d?.Invoke();
    }

    public void Language_control(int _index)//Изменение языка
    {
        Setting_PlayerPrefs.Save_parameter_value(Type_parameter_value.Language, _index);

        Language_d?.Invoke(_index);
    }

    public void Sound_effect_control(float _value)//Изменение звука эффектов
    {
        Setting_PlayerPrefs.Save_parameter_value(Type_parameter_value.Sound_effect, _value);

        Effect_sound_d?.Invoke(_value);
    }

    public void Sound_music_control(float _value)//Изменение звука музыки
    {
        Setting_PlayerPrefs.Save_parameter_value(Type_parameter_value.Sound_music, _value);

        if(Music_d != null)
        Music_d?.Invoke(_value);
    }

    public void Alert_control(bool _bool)//Изменение оповещения
    {
        Setting_PlayerPrefs.Save_parameter_bool(Type_parameter_bool.Alert_bool, _bool);
    }

    public void Vibration_control(bool _bool)//Изменение вибрации
    {
        Setting_PlayerPrefs.Save_parameter_bool(Type_parameter_bool.Vibration_bool, _bool);
    }
}
