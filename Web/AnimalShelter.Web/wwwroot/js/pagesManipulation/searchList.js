var currentPage = 1;
var currentType = 'Всички';
var currLocation = 'Всички';
var currCategory = 'Всички';
var currSex = 'Всички';
var currentOrderType = 'Id';
var currOrderDescAsc = 'desc';

window.onload = (event) => {
    searchAnimals();
};

//Add orderByEvents
document.getElementById('dateOrder').addEventListener("click", function () { searchAnimals(currentPage, 'Id'); });
document.getElementById('likeOrder').addEventListener("click", function () { searchAnimals(currentPage, 'Likes'); });
document.getElementById('ascOrder').addEventListener("click", function () { changeOrderDescAsc() });


function getDeltaPage(event, pageDelta) {
    event.preventDefault();
    searchAnimals(currentPage + pageDelta);
}

function getFirstPage(event) {
    event.preventDefault();
    searchAnimals(1);
}

function getLastPage(event, lastPage) {
    event.preventDefault();
    searchAnimals(lastPage);
}

function changeOrderDescAsc() {
    if (currOrderDescAsc == 'desc') {
        currOrderDescAsc = 'asc';
    }
    else {
        currOrderDescAsc = 'desc'
    }

    searchAnimals(currentPage, currentOrderType, currOrderDescAsc);
}

async function searchAnimals(page, orderType, orderDescAsc) {

    var location = document.getElementById('select2-Location-container').title;
    var category = document.getElementById('select2-PetStatus-container').title;
    var sex = document.getElementById('select2-Sex-container').title;
    var type = document.getElementById('select2-Type-container').title;

    if (location) {
        currLocation = location;
    }

    if (category) {
        currCategory = category;
    }

    if (sex) {
        currSex = sex;
    }

    if (page) {
        currentPage = page;
    }

    if (type) {
        currentType = type;
    }

    if (orderType) {
        currentOrderType = orderType;
    }

    if (orderDescAsc) {
        currOrderDescAsc = orderDescAsc;
    }

    var uri = `/api/SearchList?type=${currentType}&sex=${currSex}&location=${currLocation}&category=${currCategory}&page=${currentPage}&order=${currentOrderType}&orderType=${currOrderDescAsc}`;
    
    fetch(uri, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(responce => responce.json())
        .then(data => {
            let newType = data.urlInfo.type;
            let newlocation = data.urlInfo.location;
            let newSex = data.urlInfo.sex;
            let newPetStatus = data.urlInfo.petStatus;

            let newUrl = `/Search/SearchResults?Location=${newlocation}&Type=${newType}&Sex=${newSex}&PetStatus=${newPetStatus}`;
            window.history.pushState(data.urlInfo, '', newUrl);

            listPostCreatorVertical(data.animals, data);
        })
}
