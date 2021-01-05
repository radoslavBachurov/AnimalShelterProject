//Posts

var currentPage = 1;
var currentCategory = "MyPosts";
var nickName = "default";

window.onload = (event) => {
    nickName = document.getElementById("nickName").textContent;
    loadAnimals(currentPage, currentCategory, nickName);
};

function getDeltaPage(event, pageDelta) {
    event.preventDefault();
    loadAnimals(currentPage + pageDelta);
}

function getFirstPage(event) {
    event.preventDefault();
    loadAnimals(1);
}

function getLastPage(event, lastPage) {
    event.preventDefault();
    loadAnimals(lastPage);
}

async function loadAnimals(page, category) {
  
    if (page) {
        currentPage = page;
    }

    if (category) {
        currentCategory = category;
    }

    nickName = document.getElementById("nickName").textContent;

    var uri = `/api/UserInfo?category=${currentCategory}&page=${currentPage}&nick=${nickName}`;

    fetch(uri, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(responce => responce.json())
        .then(data => listPostCreatorHorizontal(data.animals, data));
}

//User info and images

$("#Images").change(function () {
    filename = this.files.length
    $("#numberPhotos").text(`${filename} снимки са избрани`);
});

function uploadInfo() {
    var token = $("#userInfo input[name=__RequestVerificationToken]").val();
    let formData = new FormData(document.getElementById("userInfo"));

    $.ajax({
        url: "/api/UserInfo/info",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            window.location.href = data.redirectToUrl;
        },
        error: function (jqXHR) {
            cleanCerificationErrors();
            cleanCerificationErrors();
            var error = $.parseJSON(jqXHR.responseText);
            let erMes;
            if (error.Nickname != undefined) {
                erMes = error.Nickname;
                $('#Nickname').before(`<p class="text-danger field-validation-error">${erMes}</p>`)
            }
            else {
                if (error.errors.Nickname) {
                    for (var i = 0; i < error.errors.Nickname.length; i++) {
                        $('#Nickname').before(`<p class="text-danger field-validation-error">${error.errors.Nickname[i]}</p>`)
                    }
                }
                if (error.errors.Sex) {
                    for (var i = 0; i < error.errors.Sex.length; i++) {
                        $('#Sex').before(`<p class="text-danger field-validation-error">${error.errors.Sex[i]}</p>`)
                    }
                }
                if (error.errors.Age) {
                    for (var i = 0; i < error.errors.Age.length; i++) {
                        $('#Age').before(`<p class="text-danger field-validation-error">${error.errors.Age[i]}</p>`)
                    }
                }
                if (error.errors.Living) {
                    for (var i = 0; i < error.errors.Living.length; i++) {
                        $('#Living').before(`<p class="text-danger field-validation-error">${error.errors.Living[i]}</p>`)
                    }
                }
                if (error.errors.Description) {
                    for (var i = 0; i < error.errors.Description.length; i++) {
                        $('#Description').before(`<p class="text-danger field-validation-error">${error.errors.Description[i]}</p>`)
                    }
                }
            }
        }
    })
}

function uploadPhotos() {
    showSpinner("uploadPhotos");

    var token = $("#uploadPhotos input[name=__RequestVerificationToken]").val();
    var dosya = $('input[type=file]');
    var form = $('#uploadPhotos')[0];
    var formData = new FormData(form);
    formData.append("file", dosya[0].files[0]);

    $.ajax({
        url: "/api/UserInfo/images",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            window.location.href = data.redirectToUrl;
        },
        error: function (jqXHR) {
            removeSpinner();
            cleanCerificationErrors();
            var error = $.parseJSON(jqXHR.responseText);

            let erMes;
            if (error.Images != undefined) {
                erMes = error.Images;
            }
            else {
                erMes = error.errors.Images;
            }
            for (var i = 0; i < erMes.length; i++) {
                $('#Images').before(`<p class="text-danger field-validation-error">${erMes}</p>`)
            }
        }
    })
}

function cleanCerificationErrors() {
    var paras = document.getElementsByClassName('field-validation-error');

    for (var i = 0; i < paras.length; i++) {
        paras[i].parentNode.removeChild(paras[i]);
    }
}

