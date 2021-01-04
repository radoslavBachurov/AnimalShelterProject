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

