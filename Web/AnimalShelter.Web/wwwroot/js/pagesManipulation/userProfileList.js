$("#Images").change(function () {
    filename = this.files.length
    $("#numberPhotos").text(`${filename} снимки са избрани`);
});

//Posts

var currentPage = 1;
var currentCategory = "MyPosts";

window.onload = (event) => {
    loadAnimals(currentPage, currentCategory);
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

    var uri = `/api/UserInfo?category=${currentCategory}&page=${currentPage}`;

    fetch(uri, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(responce => responce.json())
        .then(data => userListCreator(data.animals, data));
}