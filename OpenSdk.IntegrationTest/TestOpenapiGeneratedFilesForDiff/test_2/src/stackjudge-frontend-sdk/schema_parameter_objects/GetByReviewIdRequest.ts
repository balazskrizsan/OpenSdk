import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import {User} from "./User";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class GetByReviewIdRequest implements IOpenSdkGetable
{
    public constructor(
        private reviewId: number,
    ) {
    }

    public toOpenSdkGet()
    {
        return {
            "reviewId": this.reviewId,
        }
    }
}
