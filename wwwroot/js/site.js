window.tourScripts = {
    startAccessRequestTour: (groupId) => {
        if (!groupId || typeof groupId !== "string") {
            console.error("Invalid groupId provided to startAccessRequestTour.");
            return;
        }

        const targetSelector = `[data-tour-id="${groupId}"]`;
        console.log(`Starting tour for selector: ${targetSelector}`);
        const element = document.querySelector(targetSelector);
        console.log("Element found:", element);
        const theElement = document.getElementById('8d7987c5-64a6-487b-a336-95d38688708d');

        const tour = new Shepherd.Tour({
                useModalOverlay: true,
                defaultStepOptions: {
                    cancelIcon: { enabled: true },
                    scrollTo: { behavior: 'smooth', block: 'center' },
                    classes: 'shepherd-getchatty'
                }
            });

            tour.addStep({
                id: 'request-button',
                title: `
                        <div class="d-flex align-items-center" style="gap: 4px; padding: 0; margin: 0;">
                            <img src="/images/GetChattyLogo.png" 
                                alt="GetChatty Ltd Logo" 
                                style="height:45px; width:30px; margin: 0; padding: 0;" />
                            <span class="fw-bold" style="font-size: 1.2rem; margin: 0;">Create Access Request</span>
                        </div>
                    `,                
                text: 'Click here to request access to this group.',
                attachTo: {
                    element: theElement,
                    on: 'bottom'
                },
                buttons: [
                    {
                        text: 'Finish',
                        action: tour.complete,
                        classes: 'shepherd-getchatty-btn'
                    }
                ]
            });

            tour.start();
    }
};