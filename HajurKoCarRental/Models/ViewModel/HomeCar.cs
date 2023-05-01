﻿using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Models.ViewModel;

public class HomeCar
{
    public string? CarName { get; set; }

    public string? CarBrand { get; set; }

    public string? CarDescription { get; set; }

    public string? CarImage { get; set; }

    public string? CarModel { get; set; }

    public string? CarNumber { get; set; }

    public bool? isAvailable { get; set; }

    public bool? isRented { get; set; }

    public int? RentPrice { get; set; }
}