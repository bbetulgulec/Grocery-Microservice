using System.Text;
using manav.DtoLayer.CatalogDtos.GalleryDtos;
using manav.webUI.Services.AwsS3Services;
using manav.webUI.Services.CatalogServices.GalleryServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Gallery")]
public class GalleryController:Controller
{
    private readonly IGalleryService _galleryServices;
    private readonly ICloudStorageService _cloudStorageService;
    public GalleryController(IGalleryService galleryServices, ICloudStorageService cloudStorageService)
    {
        _galleryServices = galleryServices;
        _cloudStorageService = cloudStorageService;
    }
      [Route("Index")]
    public async Task< IActionResult> Index()
    {
        ViewbagGallery();
        var values= await _galleryServices.GetAllGalleryAsync();
        return View(values);
    }
    
    [HttpGet]
    [Route("CreateGallery")]
    public IActionResult CreateGallery()
    {
        ViewbagGallery();
        return View();
    }

    [HttpPost]
    [Route("CreateGallery")]
    public async Task<IActionResult> CreateGallery(CreateGalleryDto value)
    {
        if (value.ImageFile != null && value.ImageFile.Length > 0)
        {
            string folderPath = "HomePage/GalleryPage";
            value.GalleryUrl = await _cloudStorageService.UploadFileAsync(value.ImageFile, folderPath);
        }
        await _galleryServices.CreateGalleryAsync(value);
            return RedirectToAction("Index","Gallery",new { area = "Admin" });
     
    }

    [Route("DeleteGallery/{id}")]
    public async Task<IActionResult> DeleteGallery(string id)
    {
       await _galleryServices.DeleteGalleryAsync(id);
            return RedirectToAction("Index","Gallery",new { area = "Admin" });
    }

    [Route("UpdateGallery/{id}")]
    [HttpGet]
    public async Task<IActionResult> UpdateGallery(string id)
    {
        ViewbagGallery();
        var value=  await _galleryServices.GetByIdGalleryAsync(id);
        return View(value);
    }
    
    [Route("UpdateGallery/{id}")]
    [HttpPost]
    public async Task<IActionResult> UpdateGallery(UpdateGalleryDto updateGalleryDto)
    {
        if (updateGalleryDto.ImageFile != null && updateGalleryDto.ImageFile.Length > 0)
        {
            var existingProduct = await _galleryServices.GetByIdGalleryAsync(updateGalleryDto.GallaryId);
            if (!string.IsNullOrEmpty(existingProduct?.GalleryUrl))
            {
                await _cloudStorageService.DeleteFileAsync(existingProduct.GalleryUrl);
            }
            string folderPath = "HomePage/GalleryPage";
            updateGalleryDto.GalleryUrl = await _cloudStorageService.UploadFileAsync(updateGalleryDto.ImageFile, folderPath);
        }
        await _galleryServices.UpdateGalleryAsync(updateGalleryDto);
            return RedirectToAction("Index","Gallery",new { area = "Admin" });
    }

    public void ViewbagGallery()
    {
        ViewBag.titles = "Galeri İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Galeriler";
        ViewBag.tableList = "Galeri Listesi";
    }
}