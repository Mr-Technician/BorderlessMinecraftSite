using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BorderlessMinecraftSite.Components
{
    public partial class TypingComponent
    {
        [Parameter]
        public string[] TypingStrings { get; set; }
        int typing_line = -1;
        int typing_char = 0;
        int typing_speed = 75;
        int typing_clear_delay = 2500;
        int typing_clear_speed = 500;
        int typing_clear_after = 1000;
        int typing_clear_step = 0;

        string TypingText;
        string TypingClass;

        private async Task TypeLine()
        {
            Console.WriteLine(nameof(TypeLine));
            typing_line++;
            if (typing_line > TypingStrings.Length - 1)
            {
                typing_line = 0;
            }
            typing_char = 0;
            TypingText = "";
            await InvokeAsync(() => StateHasChanged());
            await TypeChar();
        }

        private async Task ClearLine()
        {
            Console.WriteLine(nameof(ClearLine));
            switch (typing_clear_step)
            {
                case 0:
                    TypingClass = "typing_select";
                    await InvokeAsync(() => StateHasChanged());
                    typing_clear_step++;
                    await Task.Delay(typing_clear_speed);
                    await ClearLine();
                    break;
                case 1:
                    TypingClass = "";
                    TypingText = "";
                    await InvokeAsync(() => StateHasChanged());
                    typing_clear_step++;
                    await Task.Delay(typing_clear_after);
                    await ClearLine();
                    break;
                case 2:
                    typing_clear_step = 0;
                    await TypeLine();
                    break;
                default:
                    break;
            }
        }

        private async Task TypeChar()
        {
            Console.WriteLine(nameof(TypeChar));
            if (typing_char < TypingStrings[typing_line].Length)
            {
                TypingText += TypingStrings[typing_line][typing_char];
                await InvokeAsync(() => StateHasChanged());
                typing_char++;
                await Task.Delay(typing_speed);
                await TypeChar();
            }
            else
            {
                await Task.Delay(typing_clear_delay);
                await ClearLine();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender && TypingStrings != null && TypingStrings.Length > 0)
            {
                await TypeLine();
            }
        }
    }
}
