function listPostCreatorHorizontal(animals, data) {
    if (animals == null || animals.length == 0) {
        let mainSection = mainSectionCleaner();
        let noResultDiv = document.createElement('div');
        let textEl = document.createElement('h2');
        textEl.classList.add("text-center");
        let text = document.createTextNode('НЯМА ПОСТОВЕ ЗА ПОКАЗВАНЕ');

        textEl.appendChild(text);
        noResultDiv.appendChild(textEl);
        mainSection.appendChild(noResultDiv);

        return;
    }

    let mainSection = mainSectionCleaner();

    let maintoAtach = document.createElement('div');
    maintoAtach.classList.add("range");
    maintoAtach.classList.add("spacing-30");

    let thumbnailColorCounter = 1;

    animals.forEach(el => {
        let imgSrc = el.coverPicturePath;
        let name = el.name;
        let created = el.createdOn;
        let description = el.description;
        let location = el.location;
        let sex = el.sex;
        let id = el.id;
        let likes = el.likes;
        let status = el.petStatus;
        let isApproved = el.isApproved;

        //Creating Body
        let toAtach = document.createElement('div');
        toAtach.classList.add("col-xs-12");
        toAtach.classList.add("col-sm-6");
        toAtach.classList.add("col-md-3");

        let cell = document.createElement('div');
        cell.classList.add("cell-md-6");

        let thumbnailHorizontal = document.createElement('div');
        thumbnailHorizontal.classList.add("thumbnail-boxed");
        thumbnailHorizontal.classList.add("thumbnail-boxed-vertical");

        if (thumbnailColorCounter % 3 == 0) {
            thumbnailHorizontal.classList.add("thumbnail-boxed-tan-hide");
        }
        else if (thumbnailColorCounter % 2 == 0) {
            thumbnailHorizontal.classList.add("thumbnail-boxed-blue-marguerite");
        }
        thumbnailColorCounter++;

        let boxLeft = document.createElement('div');
        boxLeft.classList.add("thumbnail-boxed-left");

        let img = document.createElement('img');
        img.classList.add("mr-3");
        img.classList.add("img-responsive");
        img.setAttribute("Alt", "");
        img.setAttribute("src", imgSrc);
        img.width = "370";
        img.height = "254";

        boxLeft.appendChild(img);
        thumbnailHorizontal.appendChild(boxLeft);

        let thumbnailBody = document.createElement('div');
        thumbnailBody.classList.add("thumbnail-boxed-body");

        //Name
        let paraName = document.createElement('p');
        paraName.classList.add("thumbnail-boxed-title");
        let textName = document.createTextNode(name);
        paraName.appendChild(textName);

        thumbnailBody.appendChild(paraName);

        let thumbnailText = document.createElement('div');
        thumbnailText.classList.add("thumbnail-boxed-text");

        //Description
        let paraDesc = document.createElement('p');
        let textDesc = document.createTextNode(description);
        paraDesc.appendChild(textDesc);

        thumbnailText.appendChild(paraDesc);
        thumbnailBody.appendChild(thumbnailText);

        let thumbnailFooter = document.createElement('div');
        thumbnailFooter.classList.add("thumbnail-boxed-footer");

        let ulData = document.createElement('ul');
        ulData.classList.add("thumbnail-boxed-meta");

        //CreatedOn
        let FirstliElement = document.createElement('li');

        let FirstSpanOne = document.createElement('span');
        FirstSpanOne.classList.add("icon");
        FirstSpanOne.classList.add("icon-xs");
        FirstSpanOne.classList.add("icon-tan-hide");
        FirstSpanOne.classList.add("material-icons-event_available");
        let FirstSpanTwo = document.createElement('span');
        let createdOn = document.createTextNode(created);
        FirstSpanTwo.appendChild(createdOn);

        FirstliElement.appendChild(FirstSpanOne);
        FirstliElement.appendChild(FirstSpanTwo);
        ulData.appendChild(FirstliElement);

        //Sex
        let SecondliElement = document.createElement('li');

        let SecondSpanOne = document.createElement('span');
        SecondSpanOne.classList.add("icon");
        SecondSpanOne.classList.add("icon-xs");
        SecondSpanOne.classList.add("icon-tan-hide");
        SecondSpanOne.classList.add("mdi");
        SecondSpanOne.classList.add("mdi-gender-male-female");
        let SecondSpanTwo = document.createElement('span');

        let sexEl = document.createTextNode(sex);
        SecondSpanTwo.appendChild(sexEl);

        SecondliElement.appendChild(SecondSpanOne);
        SecondliElement.appendChild(SecondSpanTwo);
        ulData.appendChild(SecondliElement);

        //Location
        let ThirdliElement = document.createElement('li');

        let ThirdSpanOne = document.createElement('span');
        ThirdSpanOne.classList.add("icon");
        ThirdSpanOne.classList.add("icon-xs");
        ThirdSpanOne.classList.add("icon-tan-hide");
        ThirdSpanOne.classList.add("material-icons-place");
        let ThirdSpanTwo = document.createElement('span');
        let city = document.createTextNode(location);
        ThirdSpanTwo.appendChild(city);

        ThirdliElement.appendChild(ThirdSpanOne);
        ThirdliElement.appendChild(ThirdSpanTwo);
        ulData.appendChild(ThirdliElement);


        //Likes
        let FourthliElement = document.createElement('li');

        let FourthSpanOne = document.createElement('span');
        FourthSpanOne.classList.add("icon");
        FourthSpanOne.classList.add("icon-xs");
        FourthSpanOne.classList.add("icon-tan-hide");
        FourthSpanOne.classList.add("material-icons-favorite");
        let FourthSpanTwo = document.createElement('span');
        let likesEl = document.createTextNode(likes);
        FourthSpanTwo.appendChild(likesEl);

        FourthliElement.appendChild(FourthSpanOne);
        FourthliElement.appendChild(FourthSpanTwo);
        ulData.appendChild(FourthliElement);

        //Category
        let FifthliElement = document.createElement('li');

        let FifthSpanOne = document.createElement('span');
        FifthSpanOne.classList.add("icon");
        FifthSpanOne.classList.add("icon-xs");
        FifthSpanOne.classList.add("icon-tan-hide");
        FifthSpanOne.classList.add("mdi");
        FifthSpanOne.classList.add("mdi-paw");
        let FifthSpanTwo = document.createElement('span');

        let category = document.createTextNode(status);
        FifthSpanTwo.appendChild(category);

        FifthliElement.appendChild(FifthSpanOne);
        FifthliElement.appendChild(FifthSpanTwo);
        ulData.appendChild(FifthliElement);

        //IsApproved
        let SixliElement = document.createElement('li');

        let SixSpanOne = document.createElement('span');
        SixSpanOne.classList.add("icon");
        SixSpanOne.classList.add("icon-xs");
        SixSpanOne.classList.add("icon-tan-hide");
        SixSpanOne.classList.add("mdi");
        SixSpanOne.classList.add("mdi-check-all");
        let SixSpanTwo = document.createElement('span');

        let approval = document.createTextNode(isApproved);
        SixSpanTwo.appendChild(approval);

        SixliElement.appendChild(SixSpanOne);
        SixliElement.appendChild(SixSpanTwo);
        ulData.appendChild(SixliElement);

        thumbnailFooter.appendChild(ulData);
        thumbnailBody.appendChild(thumbnailFooter);

        //More Button
        let anker = document.createElement('a');
        anker.classList.add('btn');
        anker.classList.add('btn-blue-marguerite');
        anker.classList.add('btn-effect-anis');
        anker.classList.add('wow');
        anker.classList.add('fadeInUpSmall');
        anker.href = `/Pet/PetProfile?id=${id}`;
        anker.setAttribute("data-wow-delay", "0.2s");
        anker.setAttribute("data-wow-duration", ".75s");

        let aTextNode = document.createTextNode("Виж повече");
        anker.appendChild(aTextNode);

        thumbnailBody.appendChild(anker);
        thumbnailHorizontal.appendChild(thumbnailBody);
        cell.appendChild(thumbnailHorizontal);

        toAtach.appendChild(cell);
        maintoAtach.appendChild(toAtach);
    })

    mainSection.appendChild(maintoAtach);
    //Pagination
    let pageList = document.createElement('ul');
    pageList.classList.add("pagination-custom");

    //Previous Page
    let liPrevious = document.createElement('li');

    if (data.hasPreviousPage) {
        liPrevious.classList.add("enabled");
    }
    else {
        liPrevious.classList.add("disabled");
    }

    let ankerPrevious = document.createElement('a');
    ankerPrevious.setAttribute("aria-label", "Previous");
    ankerPrevious.setAttribute("id", "PreviousPage");

    liPrevious.appendChild(ankerPrevious);
    pageList.appendChild(liPrevious);

    //FirstPage

    let liFirstPage = document.createElement('li');
    if (data.hasPreviousPage) {
        liFirstPage.classList.add("enabled");
    }
    else {
        liFirstPage.classList.add("disabled");
    }
    let liFirstPageAnker = document.createElement('a');
    liFirstPageAnker.setAttribute("id", "pageFirstPage");
    liFirstPageAnker.appendChild(document.createTextNode("<<<"));
    liFirstPage.appendChild(liFirstPageAnker);
    pageList.appendChild(liFirstPage);


    //Page Number - 3
    if (data.pageNumber - 3 > 0) {
        let liPreviousThree = document.createElement('li');
        let liPreviousThreeAnker = document.createElement('a');
        liPreviousThreeAnker.setAttribute("id", "pageMinusThree");
        liPreviousThreeAnker.appendChild(document.createTextNode(data.pageNumber - 3));
        liPreviousThree.appendChild(liPreviousThreeAnker);
        pageList.appendChild(liPreviousThree);
    }

    //Page Number - 2
    if (data.pageNumber - 2 > 0) {
        let liPreviousTwo = document.createElement('li');
        let liPreviousTwoAnker = document.createElement('a');
        liPreviousTwoAnker.setAttribute("id", "pageMinusTwo");
        liPreviousTwoAnker.appendChild(document.createTextNode(data.pageNumber - 2));
        liPreviousTwo.appendChild(liPreviousTwoAnker);
        pageList.appendChild(liPreviousTwo);
    }

    //Page Number - 1
    if (data.pageNumber - 1 > 0) {
        let liPreviousOne = document.createElement('li');
        let liPreviousOneAnker = document.createElement('a');
        liPreviousOneAnker.setAttribute("id", "pageMinusOne");
        liPreviousOneAnker.appendChild(document.createTextNode(data.pageNumber - 1));
        liPreviousOne.appendChild(liPreviousOneAnker);
        pageList.appendChild(liPreviousOne);
    }

    //Current Page
    let liCurrentpage = document.createElement('li');
    liCurrentpage.classList.add("disabled");
    let liCurrentpageAnker = document.createElement('a');
    liCurrentpageAnker.appendChild(document.createTextNode(data.pageNumber));
    liCurrentpage.appendChild(liCurrentpageAnker);
    pageList.appendChild(liCurrentpage);

    //Page Number + 1
    if (data.pageNumber + 1 <= data.pagesCount) {
        let liPlusOne = document.createElement('li');
        let liPlusOneAnker = document.createElement('a');
        liPlusOneAnker.setAttribute("id", "pagePlusOne");
        liPlusOneAnker.appendChild(document.createTextNode(data.pageNumber + 1));
        liPlusOne.appendChild(liPlusOneAnker);
        pageList.appendChild(liPlusOne);
    }

    //Page Number + 2
    if (data.pageNumber + 2 <= data.pagesCount) {
        let liPlusTwo = document.createElement('li');
        let liPlusTwoAnker = document.createElement('a');
        liPlusTwoAnker.setAttribute("id", "pagePlusTwo");
        liPlusTwoAnker.appendChild(document.createTextNode(data.pageNumber + 2));
        liPlusTwo.appendChild(liPlusTwoAnker);
        pageList.appendChild(liPlusTwo);
    }

    //Page Number + 3
    if (data.pageNumber + 3 <= data.pagesCount) {
        let liPlusThree = document.createElement('li');
        let liPlusThreeAnker = document.createElement('a');
        liPlusThreeAnker.setAttribute("id", "pagePlusThree");
        liPlusThreeAnker.appendChild(document.createTextNode(data.pageNumber + 3));
        liPlusThree.appendChild(liPlusThreeAnker);
        pageList.appendChild(liPlusThree);
    }

    //Last Page
    let liLastPage = document.createElement('li');
    if (data.hasNextPage) {
        liLastPage.classList.add("enabled");
    }
    else {
        liLastPage.classList.add("disabled");
    }
    let liLastPageAnker = document.createElement('a');
    liLastPageAnker.setAttribute("id", "pageLastPage");
    liLastPageAnker.appendChild(document.createTextNode(">>>"));
    liLastPage.appendChild(liLastPageAnker);
    pageList.appendChild(liLastPage);

    //Next Page
    let liNext = document.createElement('li');

    if (data.hasNextPage) {
        liNext.classList.add("enabled");
    }
    else {
        liNext.classList.add("disabled");
    }

    let ankerNext = document.createElement('a');
    ankerNext.setAttribute("aria-label", "Next");
    ankerNext.setAttribute("id", "NextPage");

    liNext.appendChild(ankerNext);
    pageList.appendChild(liNext);

    ////
    mainSection.appendChild(pageList);

    document.getElementById('NextPage').addEventListener("click", function () { getDeltaPage(event, 1); });
    document.getElementById('PreviousPage').addEventListener("click", function () { getDeltaPage(event, -1); });

    if (data.pageNumber - 1 > 0) {
        document.getElementById('pageMinusOne').addEventListener("click", function () { getDeltaPage(event, -1); });
    }
    if (data.pageNumber - 2 > 0) {
        document.getElementById('pageMinusTwo').addEventListener("click", function () { getDeltaPage(event, -2); });
    }
    if (data.pageNumber - 3 > 0) {
        document.getElementById('pageMinusThree').addEventListener("click", function () { getDeltaPage(event, -3); });
    }
    if (data.pageNumber + 1 <= data.pagesCount) {
        document.getElementById('pagePlusOne').addEventListener("click", function () { getDeltaPage(event, +1); });
    }
    if (data.pageNumber + 2 <= data.pagesCount) {
        document.getElementById('pagePlusTwo').addEventListener("click", function () { getDeltaPage(event, +2); });
    }
    if (data.pageNumber + 3 <= data.pagesCount) {
        document.getElementById('pagePlusThree').addEventListener("click", function () { getDeltaPage(event, +3); });
    }

    document.getElementById('pageFirstPage').addEventListener("click", function () { getFirstPage(event); });
    document.getElementById('pageLastPage').addEventListener("click", function () { getLastPage(event, data.pagesCount); });

}

function mainSectionCleaner() {
    let myPostsSection = document.getElementById('MyPosts');
    myPostsSection.innerHTML = '';
    let likedPostsSection = document.getElementById('LikedPosts');

    if (likedPostsSection != null) {
        likedPostsSection.innerHTML = '';
    }

    let mainSection = document.getElementById(currentCategory);

    return mainSection;
}