export class TeethMap {

    constructor(objectId, tableId, clientId) {
        this.objectId = objectId;
        this.tableId = tableId;
        this.clientId = clientId;
    }

    drawTeethMap_bcp() {
        console.log('Start draw');
        var element = $('#' + this.objectId);
        //var element = document.getElementById(this.objectId);
        console.log(element);
        if (element != null) {/*
            var id = '#' + this.tableId;
            var tableDOM = document.createElement("table");
            tableDOM.setAttribute("id", id);
            tableDOM.classList.add("table_of_teeth");

            for (let tr = 1; tr <= 2; tr++) {
                var classNameTr = tr == 1 ? 'top' : 'bottom';
                var trDOM = document.createElement("tr");
                trDOM.classList.add(classNameTr);

                for (let td = 0; td < 2; td++) {

                    var classNameTd = td == 0 ? 'left' : 'right';
                    var tdDOM = document.createElement("td");
                    tdDOM.classList.add("column");

                    var divDOM = document.createElement("div");
                    divDOM.classList.add("flex");

                    for (let svg = 1; svg <= 8; svg++) {
                        var cell = 0;
                        if (tr == 1) {
                            cell = tr + td;
                        }
                        else {
                            cell = tr + 2 - td;
                        }
                        var toothId = 'tooth_' + cell + svg;

                        var svgDOM = document.createElement("svg");
                        svgDOM.setAttribute("id", toothId);
                        svgDOM.setAttribute("data-src", 'teeth/' + classNameTr + '_' + classNameTd + '/tooth_' + cell + svg + '.svg');
                        svgDOM.setAttribute("data-cache", "disabled");
                        svgDOM.classList.add("tooth");
                        svgDOM.classList.add("teeth_" + classNameTr + '_' + svg)

                        divDOM.appendChild(svgDOM);
                        tdDOM.appendChild(divDOM);
                    }

                    trDOM.appendChild(tdDOM);
                }

                tableDOM.appendChild(trDOM);
            }

            element.appendChild(tableDOM);*/
            
            var id = '#' + this.tableId;
            var content = '<table id="' + id + '" class="table_of_teeth">';
            for (let tr = 1; tr <= 2; tr++) {
                var classNameTr = tr == 1 ? 'top' : 'bottom';

                content += '<tr class="' + classNameTr + '">';
                for (let td = 0; td < 2; td++) {
                    var classNameTd = td == 0 ? 'left' : 'right';
                    content += '<td class="column"><div class="flex">';

                    for (let svg = 1; svg <= 8; svg++) {
                        var cell = 0;
                        if (tr == 1) {
                            cell = tr + td;
                        }
                        else {
                            cell = tr + 2 - td;
                        }
                        var toothId = 'tooth_' + cell + svg;
                        var svg_content = '<svg id="' + toothId +
                            '" data-src="teeth/' + classNameTr + '_' + classNameTd + '/tooth_' + cell + svg + '.svg" data-cache="disabled" class="tooth teeth_' +
                            classNameTr + '_' + svg + '"></svg>';
                        
                        $("body").on("click", "#" + toothId, function (event) {
                            showModal(event);
                        });
                        
                        $("body").on("contextmenu", "#" + toothId, function (event) {
                            showContextMenu(event);
                        });
                        
                        content += svg_content;
                    }

                    content += '</div></td>';
                }
                content += '</tr>';
            }
            content += "</table>"
            //console.log(content);

            element.append(content);
            

        }
        else {
            console.log('Object not found', element);
        }
    }

    drawTeethMap() {
        console.log('Start draw');
        //var element = $('#' + this.objectId);
        var element = document.getElementById(this.objectId);
        console.log(element);
        if (element != null) {
            var id = '#' + this.tableId;
            var cardColDOM = document.createElement("div");
            cardColDOM.classList.add("col-5");

            var tableDOM = document.createElement("table");
            tableDOM.setAttribute("id", id);
            tableDOM.classList.add("table_of_teeth");

            var tableBodyDOM = document.createElement("tbody");
            

            for (let tr = 1; tr <= 2; tr++) {
                var classNameTr = tr == 1 ? 'top' : 'bottom';
                var trDOM = document.createElement("tr");
                trDOM.classList.add(classNameTr);

                for (let td = 0; td < 2; td++) {

                    var classNameTd = td == 0 ? 'left' : 'right';
                    var tdDOM = document.createElement("td");
                    tdDOM.classList.add("column");

                    var divDOM = document.createElement("div");
                    divDOM.classList.add("flex");

                    for (let svg = 1; svg <= 8; svg++) {
                        var cell = 0;
                        if (tr == 1) {
                            cell = tr + td;
                        }
                        else {
                            cell = tr + 2 - td;
                        }
                        var toothIdNum = '' + cell + svg;

                        var found = this.apiData.find((x, ind) => x._Tooth.toothId == toothIdNum);

                        var toothId = 'tooth_' + cell + svg;

                        var svgDOM = document.createElementNS("http://www.w3.org/2000/svg", "svg");
                        svgDOM.setAttribute("id", toothId);
                        svgDOM.setAttribute("data-src", '../../teeth/teeth_svg/tooth_' + cell + svg + '.svg');
                        svgDOM.setAttribute("data-cache", "disabled");
                        svgDOM.classList.add("tooth");
                        svgDOM.classList.add("teeth_" + classNameTr + '_' + svg)

                        if (found != null) {
                            let color = found._ToothState != null ? found._ToothState.toothStateColor : "";
                            console.log('Found tooth:', found, color);
                            
                            if (color != "") {
                                console.log("Add class", color)
                                svgDOM.classList.add(color);
                            }
                        }

                        svgDOM.addEventListener("contextmenu", function (event) {
                            showContextMenu(event);
                        });
                        let map = this;
                        svgDOM.addEventListener("click", function (event){
                            
                            var toothFileName = event.target.nearestViewportElement.id;
                            var toothId = toothFileName.replace('tooth_', '');
                            console.log("Click", event, toothId, map);
                            map.getClientsToothByAPI(toothId);

                        });

                        divDOM.appendChild(svgDOM);
                        tdDOM.appendChild(divDOM);
                    }

                    trDOM.appendChild(tdDOM);
                }

                tableBodyDOM.appendChild(trDOM);
            }
            tableDOM.appendChild(tableBodyDOM);
            cardColDOM.append(tableDOM);
            element.appendChild(cardColDOM);
            console.log('End draw');

        }
        else {
            console.log('Object not found', element);
        }
    }

    setToothType(toothId, stat) {
        _setToothType(toothId, stat);
    }

    getClientsTeethByAPI() {
        let map = this;
        $.ajax({
            type: 'GET',
            url: this.api + this.clientId,
            success: function (data) {
                map.setDataToMap(data);
                map.drawTeethMap();
                
            },
            contentType: "application/json",
            dataType: 'json'
        });
    }

    getClientsToothByAPI(toothId) {
        let map = this;
        $.ajax({
            type: 'GET',
            url: this.api + this.clientId + '/' + toothId,
            success: function (data) {
                console.log(this);
                showToothDetails(data, map.objectId);

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Error: " + errorThrown);
            },
            contentType: "application/json",
            dataType: 'json'
        });
    }

    setAPI(api) {
        this.api = api;
    }

    setDataToMap(data) {
        this.apiData = data;
        /*
        console.log(data);

        data.forEach(element => {
            console.log(element);
        });
        */
    }

}

export const ToothStat = {
    Base: 'base-color',
    Repaired: 'repaired-color',
    Removed: 'removed-color',
    Empty: 'empty-color',
    Implantat: 'implantat-color',
    TemporaryImplantatCrown: 'temporary-crown-implantat-color',
    PermanentImplantatCrown: 'permanent-crown-implantat-color'

};

function clearColor(tooth) {
    tooth.classList.remove(ToothStat.Base);
    tooth.classList.remove(ToothStat.Repaired);
    tooth.classList.remove(ToothStat.Removed);
    tooth.classList.remove(ToothStat.Empty);
    tooth.classList.remove(ToothStat.Implantat);

}

function _setToothType(toothId, stat) {
    console.log('setToothType: ', toothId);
    var tooth = document.getElementById(toothId);
    console.log(tooth);
    if (tooth != null) {
        clearColor(tooth);
        console.log(stat);
        tooth.classList.add(stat);
    }
}

function showToothDetails(data, objectId) {
    console.log("showToothDetails", data, objectId);
    var element = document.getElementById(objectId);
    
    if (element != null) {
    }
        var id = 'tooth_details';
        console.log(id);
        
        var cardDOM = document.getElementById(id);
        console.log(cardDOM);
        if (cardDOM != null)
        {
            document.getElementById("tooth_details").remove();
        }
        cardDOM = document.createElement("div");
        cardDOM.setAttribute("id", id);
        cardDOM.classList.add("tooth_details");
        cardDOM.classList.add("card");
        cardDOM.classList.add("col-5");

        let cardBodyDOM = document.createElement("div");
        cardBodyDOM.classList.add("card-body");

        let cardHeadRowDOM = document.createElement("div");
        cardHeadRowDOM.classList.add("row");

        let col1DOM = document.createElement("div");
        col1DOM.classList.add("col");
        col1DOM.classList.add("col-2");
        col1DOM.classList.add("me-2");

        let side = (data._Tooth.toothId + '').substring(0, 1);

        let sideText = side == "1" || side == "2" ? "top" : "bottom";
        console.log(side);

        let toothId = "tooth_details_" + data._Tooth.toothId;
        var svgDOM = document.createElementNS("http://www.w3.org/2000/svg", "svg");
        svgDOM.setAttribute("id", toothId);
        svgDOM.setAttribute("data-src", '../../teeth/teeth_svg/tooth_' + data._Tooth.toothId + '.svg');
        svgDOM.setAttribute("data-cache", "disabled");
        svgDOM.classList.add("tooth");
        svgDOM.classList.add("float-start");
        svgDOM.classList.add("teeth_" + sideText + '_' + data._Tooth.currentNumber);
        svgDOM.classList.add(data._ToothState.toothStateColor)

        col1DOM.append(svgDOM);
        cardHeadRowDOM.append(col1DOM);

        let col2DOM = document.createElement("div");
        col2DOM.classList.add("col");
        col2DOM.classList.add("col-8");

        let h5DOM = document.createElement("h5");
        console.log(data._Tooth.name);
        let h5TextDOM = document.createTextNode(data._Tooth.name);
        h5DOM.append(h5TextDOM);

        let h6DOM = document.createElement("h6");
        console.log(data._Tooth.toothId);
        let h6TextDOM = document.createTextNode(data._Tooth.toothId);
        h6DOM.append(h6TextDOM);

        console.log(h5DOM);
        console.log(h6DOM);
        col2DOM.append(h5DOM);
        col2DOM.append(h6DOM);

        cardHeadRowDOM.append(col2DOM);

        let col3DOM = document.createElement("div");
        col3DOM.classList.add("col");

        col3DOM.style.textAlign = "right";
        col3DOM.style.paddingLeft = "0px";

        let buttonDOM = document.createElement("button");
        buttonDOM.setAttribute("type", "button");
        buttonDOM.setAttribute("aria-label", "Close");
        buttonDOM.classList.add("btn-close");

        buttonDOM.addEventListener("click", function () {
            document.getElementById("tooth_details").remove();
        });

        col3DOM.append(buttonDOM);
        cardHeadRowDOM.append(col3DOM);
        cardBodyDOM.append(cardHeadRowDOM);

        let cardRowDOM = document.createElement("div");
        cardRowDOM.classList.add("row");

        let hrDOM = document.createElement("hr");
        hrDOM.classList.add("my-2");
        cardRowDOM.append(hrDOM);

        let pDOM = document.createElement("p");
        pDOM.classList.add("card-text");
        pDOM.append(document.createTextNode(data._ToothState.toothStateName));
        cardRowDOM.append(pDOM);

        cardBodyDOM.append(cardRowDOM);
            
        cardDOM.append(cardBodyDOM);
        element.append(cardDOM);
    

}

function showModal(event) {
    console.log(event.currentTarget);
    alert(event.currentTarget.id);
}

function showContextMenu(event) {
    event.preventDefault();
    var targetElementId = event.target.nearestViewportElement.id;
    console.log(event.target.nearestViewportElement.id);
    var contextMenu = document.getElementById('context-menu'); /*$('#context-menu');*/
    //console.log(contextMenu);

    const { clientX: mouseX, clientY: mouseY } = event;
    //console.log(mouseY);
    const body = document.querySelector('body');

    const {
        left: scopeOffsetX,
        top: scopeOffsetY
    } = body.getBoundingClientRect();

    var normalizeX = normalizePozition(mouseX, scopeOffsetX, contextMenu.clientWidth, body.clientWidth);
    var normalizeY = normalizePozition(mouseY, scopeOffsetY, contextMenu.clientHeight, body.clientHeight);

    //console.log('X: ', mouseX, ' NormX: ', normalizeX);
    //console.log('Y: ', mouseY, ' NormY: ', normalizeY);
    contextMenu.style.top = normalizeY + 'px';
    contextMenu.style.left = normalizeX + 'px';

    var items = document.getElementsByClassName("context-menu-item");
    //console.log("items", items);
    if (items.length > 0) {
        for (let i = 0; i < items.length; i++) {
            let item = items[i];

            var id = item.id;
            console.log(id);
            item.replaceWith(item.cloneNode(true));
            var itemDOM = document.getElementById(item.id);
            
            itemDOM.addEventListener("click", function (event) {

                console.log(targetElementId, stat, event);
                var stat = ToothStat.Base;
                switch (event.target.id) {
                    case "context-menu-item1": stat = ToothStat.Base; console.log('1'); break;
                    case "context-menu-item2": stat = ToothStat.Repaired; console.log('2'); break;
                    case "context-menu-item3": stat = ToothStat.Removed; console.log('3'); break;
                    case "context-menu-item4": stat = ToothStat.Empty; console.log('4'); break;
                    case "context-menu-item5": stat = ToothStat.Implantat; console.log('5'); break;
                    default: stat = ToothStat.Base; console.log('default'); break;
                }
                _setToothType(targetElementId, stat);
                hideContextMenu();
            }, false);

            
        }
    }/*
    $("#context-menu").on("click", "#context-menu-item1", function (ev) {
        console.log(event);
        _setToothType(event.currentTarget.id, ToothStat.Base);
        hideContextMenu();
    });
    
    $("#context-menu").on("click", "#context-menu-item2", function (ev) {
        console.log(event);
        _setToothType(event.currentTarget.id, ToothStat.Repaired);
        hideContextMenu();
    });
    
    $("#context-menu").on("click", "#context-menu-item3", function (ev) {
        console.log(event);
        _setToothType(event.currentTarget.id, ToothStat.Removed);
        hideContextMenu();
    });
    
    $("#context-menu").on("click", "#context-menu-item4", function (ev) {
        console.log(event);
        _setToothType(event.currentTarget.id, ToothStat.Empty);
        hideContextMenu();
    });
    $("#context-menu").on("click", "#context-menu-item5", function (ev) {
        console.log(event);
        _setToothType(event.currentTarget.id,, ToothStat.Implantat);
        hideContextMenu();
    });
    */
    body.addEventListener("click", (e) => {
        if (e.target.offsetParent != contextMenu) {
            contextMenu.classList.remove("visible");
        }
    });

    setTimeout(() => {
        console.log('Timeout');
        contextMenu.classList.remove("visible");
    }, 10000)

    contextMenu.classList.add("visible");

    //alert('Right click:' + event.currentTarget.id);
}

export function hideContextMenu() {
    var contextMenu = document.getElementById('context-menu');
    contextMenu.classList.remove("visible");
}

function normalizePozition(mouse, scopeOffset, contextMenuSize, clientSize) {
    //console.log('mouse', mouse, 'scopeOffset', scopeOffset, 'contextMenuSize', contextMenuSize, 'clientSize', clientSize)
    const scope = mouse - scopeOffset;

    const outOfBounds = mouse + contextMenuSize > clientSize;

    let normalized = mouse;

    if (outOfBounds) {
        normalized = scopeOffset + clientSize - contextMenuSize;
    }

    return normalized;
};

