﻿using System;
using System.ComponentModel;
using System.Linq;
using Prism.Mvvm;
using WpfFluentValidation.Validators;

namespace WpfFluentValidation.Models;

/// <summary>
///     学生实体
///     继承BaseClasss,即继承属性变化接口INotifyPropertyChanged
///     实现IDataErrorInfo接口，用于FluentValidation验证，必须实现此接口
/// </summary>
public class Student : BindableBase, IDataErrorInfo
{
    private int _age;
    private string? _name;
    private string? _zip;
    private readonly StudentValidator _validator = new();

    public string? Name
    {
        get => _name;
        set
        {
            if (value != _name)
            {
                _name = value;
                SetProperty(ref _name, value);
            }
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value != _age)
            {
                _age = value;
                SetProperty(ref _age, value);
            }
        }
    }

    public string? Zip
    {
        get => _zip;
        set
        {
            if (value != _zip)
            {
                _zip = value;
                SetProperty(ref _zip, value);
            }
        }
    }


    public string this[string columnName]
    {
        get
        {
            var validateResult = _validator.Validate(this);
            if (validateResult.IsValid)
            {
                return string.Empty;
            }

            var firstOrDefault =
                validateResult.Errors.FirstOrDefault(error => error.PropertyName == columnName);
            return firstOrDefault == null ? string.Empty : firstOrDefault.ErrorMessage;
        }
    }

    public string Error
    {
        get
        {
            var validateResult = _validator.Validate(this);
            if (validateResult.IsValid)
            {
                return string.Empty;
            }

            var errors = string.Join(Environment.NewLine, validateResult.Errors.Select(x => x.ErrorMessage).ToArray());
            return errors;
        }
    }
}