import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class PostCompany implements IOpenSdkPostable
{
    public constructor(
        private company: string,
        private address: string,
    ) {
    }

    public toOpenSdkPost()
    {
        return {
            "company": this.company,
            "address": this.address,
        }
    }
}
