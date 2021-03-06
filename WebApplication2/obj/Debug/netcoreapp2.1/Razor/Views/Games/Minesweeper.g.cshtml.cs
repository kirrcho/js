#pragma checksum "E:\Projects\WebApplication2\WebApplication2\Views\Games\Minesweeper.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e3c8887a275ac2ee884456ce16001fd7583282a8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Games_Minesweeper), @"mvc.1.0.view", @"/Views/Games/Minesweeper.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Games/Minesweeper.cshtml", typeof(AspNetCore.Views_Games_Minesweeper))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\Projects\WebApplication2\WebApplication2\Views\_ViewImports.cshtml"
using WebApplication2;

#line default
#line hidden
#line 2 "E:\Projects\WebApplication2\WebApplication2\Views\_ViewImports.cshtml"
using WebApplication2.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e3c8887a275ac2ee884456ce16001fd7583282a8", @"/Views/Games/Minesweeper.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b36aee4455a440795f240a74431c307640c545e", @"/Views/_ViewImports.cshtml")]
    public class Views_Games_Minesweeper : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApplication2.App.Models.MinesweeperBoard>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(52, 3078, true);
            WriteLiteral(@"<div style=""border:5px solid;padding-bottom:800px"">
    <div style=""position:absolute;margin:20px 0px 0px 60px;font-size:50px"" id=""minesRemaining"">99</div>
    <div style=""position:absolute;margin:20px 0px 0px 810px;font-size:50px"" id=""time"" class=""stopped"">00:00</div>
    <div id=""game"" oncontextmenu=""return false;"">
    </div>
    <style>
        .unknown, .flagged {
            width: 25px;
            height: 25px;
            display: inline-block;
            background-color: #BCB8B8;
            border-color: black;
            border-width: 1px;
            border-radius: 5px;
            border-style: solid;
            margin-bottom: -3.2px;
            margin-right: 0.8px;
        }

        .empty {
            width: 25px;
            height: 25px;
            display: inline-block;
            background-color: #BCB8B8;
            border-color: #e1e1e1;
            border-width: 1px;
            border-style: solid;
            margin-bottom: -3.2px;
            mar");
            WriteLiteral(@"gin-right: 0.8px;
            text-align: center;
            vertical-align: top;
            line-height: 25px;
        }

        #game {
            width: 840px;
            height:447.5px;
            margin-top: 100px;
            margin-left: 60px;
            position: absolute;
            background-color: #BCB8B8;
            border: 0.1px;
            border-color: black;
            border-style: solid
        }
    </style>
    <script>
        const Mines_In_Game = 99;
        const Rows_In_Game = 16;
        const Cols_In_Game = 30;

        $(document).ready(function () {
            let game = $(""#game"");
            for (let i = 1; i <= Rows_In_Game; i++) {
                for (let k = 1; k <= Cols_In_Game; k++) {
                    game.append(`<btn id=""${k}_${i}"" class=""unknown"" style=""position:absolute;margin-left:${(k - 1) * 28}px;margin-top:${(i - 1) * 28}px""></btn>`);
                };
            };
            let clock;
            $('.unknown').mo");
            WriteLiteral(@"usedown(function (e) {
                e.preventDefault();
                e.stopPropagation();
                if ($(""#time"").hasClass('stopped')) {
                    clock = createClock();
                }
                let button = e.target.tagName == ""IMG"" ? e.target.parentElement : e.target;

                let minesRemaining = $('#minesRemaining')[0].textContent;
                if (minesRemaining == 1 || minesRemaining == -1) {
                    let flagged = """";
                    for (let flag of $('.flagged')) {
                        flagged += flag.id + "";"";

                    }
                }

                if (e.which == 1) {
                    if (true) {
                        let flagged = $('.flagged');
                        let flaggedSquares = [];
                        for (let square of flagged) {
                            flaggedSquares.push(square.id);
                        }
                        let json = JSON.stringify({ encodedBo");
            WriteLiteral("ard: \"");
            EndContext();
            BeginContext(3131, 18, false);
#line 85 "E:\Projects\WebApplication2\WebApplication2\Views\Games\Minesweeper.cshtml"
                                                              Write(Model.EncodedBoard);

#line default
#line hidden
            EndContext();
            BeginContext(3149, 2880, true);
            WriteLiteral(@""", flagged: flaggedSquares, currentId: e.currentTarget.id });
                        $.ajax(""/Games/Minesweeper"", {
                            type: ""post"",
                            data: json,
                        contentType: ""application/json; charset=utf8""
                    }).then(function (data) {
                        if (data.gameEnded) {
                            clearInterval(clock);
                            endGame(button, data.mines);
                        } else {
                            RevealTiles(data.mines);
                        }
                    });
                    }
                } else if (e.which == 3 && button.className != ""empty"") {
                    
                    if (button.className == ""flagged"") {
                        $('#minesRemaining')[0].textContent = +$('#minesRemaining')[0].textContent + 1;
                        button.innerHTML = """";
                        button.className = ""unknown"";
                    }");
            WriteLiteral(@" else {
                        $('#minesRemaining')[0].textContent = +$('#minesRemaining')[0].textContent - 1;
                        $(button).append(`<img src=""/images/flag.png"" style=""margin-left:5px;max-width:100%;max-height:90%;opacity: 0.5;""></img>`);
                        button.className = ""flagged"";
                    }
                }
            });
        });

        function RevealTiles(mines) {
            for (let tile of mines) {
                let mine = $(`#${tile.currentId}`)[0];
                mine.className = ""empty"";
                if (tile.numberToShow == 0) {
                    mine.innerHTML = """";
                } else {
                    mine.textContent = tile.numberToShow;
                }
            }
        }

        function endGame(endingElement, mines) {
            $(endingElement).css('background-color', 'red');
            for (let i = 0; i < Mines_In_Game; i++) {
                let element = $(`#${mines[i].currentId}`);
       ");
            WriteLiteral(@"         if (element.hasClass(""flagged"")) {
                    element[0].innerHTML = '';
                    element.append(`<img src=""/images/locatedMine.jpg"" style=""max-width:100%;max-height:90%;""></img>`);
                } else {
                    element.append(`<img src=""/images/mine.jpg"" style=""max-width:100%;max-height:90%;""></img>`);
                }
            }
        }

        function createClock() {
            let count = 0;
            $('#time')[0].className = ""running"";
            let clock = setInterval(function () {
                count++;
                $('#time')[0].textContent = `${Math.floor(count / 600)}${Math.floor((count % 600) / 60)}:${Math.floor((count % 60) / 10)}${(count % 60) % 10}`
            }, 1000);
            return clock;
        }
    </script>
</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication2.App.Models.MinesweeperBoard> Html { get; private set; }
    }
}
#pragma warning restore 1591
