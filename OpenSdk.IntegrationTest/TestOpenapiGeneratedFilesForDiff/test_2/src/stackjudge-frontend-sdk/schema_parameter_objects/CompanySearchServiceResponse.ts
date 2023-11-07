import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class CompanySearchServiceResponse
{
    public constructor(
        private companies: valueObject.Company[],
        private companyGroups: object,
        private paginator: valueObject.PaginatorItem[],
        private newSeekId: number,
        private companyStatistics: object,
        private companyAddresses: object,
        private companyAddressMaps: object,
        private companyReviews: object,
        private companyOwners: object,
        private companyUsers: object,
    ) {
    }
}
