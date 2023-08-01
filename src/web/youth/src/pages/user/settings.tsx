import { zodResolver } from "@hookform/resolvers/zod";
import { useSession } from "next-auth/react";
import { useCallback, useEffect, type ReactElement } from "react";
import { useForm, type FieldValues } from "react-hook-form";
import { toast } from "react-toastify";
import zod from "zod";
import { type UserProfileRequest } from "~/api/models/user";
import { patchUser } from "~/api/user";
import MainBackButtonLayout from "~/components/Layout/MainBackButton";
import { ApiErrors } from "~/components/apiErrors";
import { useCountries, useGenders } from "~/hooks/api/lookups";
import { useGetUser } from "~/hooks/api/user";
import { type NextPageWithLayout } from "../_app";

const Settings: NextPageWithLayout = () => {
  const { data: genders } = useGenders();
  const { data: countries } = useCountries();
  const { data: user } = useGetUser();
  const { data: session, update } = useSession();

  // const [submittedValues, setSubmittedValues] =
  //   useState<UserProfileRequest | null>(null);
  // usePatchUser(submittedValues);

  const schema = zod.object({
    email: zod.string().email().min(1, "Email is required"),
    firstName: zod.string().min(1, "First name is required"),
    surname: zod.string().min(1, "Last name is required"),
    displayName: zod.string().min(1, "Display name is required"),
    phoneNumber: zod
      .string()
      .min(1, "Phone number is required")
      .regex(
        /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/,
        "Phone number is invalid"
      ),
    countryId: zod.string().min(1, "Country is required"),
    countryOfResidenceId: zod
      .string()
      .min(1, "Country of residence is required"),
    genderId: zod.string().min(1, "Gender is required"),
    dateOfBirth: zod.coerce
      .date({
        required_error: "Please select a date and time",
        invalid_type_error: "That's not a date!",
      })
      .max(new Date(), { message: "Date of birth cannot be in the future" }),
    resetPassword: zod.boolean(),
  });

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(schema),
  });

  // set default values
  useEffect(() => {
    //HACK: ISO 8601 date needs to in the YYYY-MM-DD format for the input(type=date) to display correctly
    if (user?.dateOfBirth != null) {
      let date = new Date(user?.dateOfBirth);
      user.dateOfBirth = date.toISOString().slice(0, 10);
    }
    reset(user);
  }, [user, reset]);

  // form submission handler
  const onSubmit = useCallback(
    async (data: FieldValues) => {
      // update api
      // setSubmittedValues(data as UserProfileRequest);

      try {
        await patchUser(data as UserProfileRequest);
      } catch (error: any) {
        toast(<ApiErrors error={error} />, {
          type: "error",
          toastId: "patchUserProfileError",
          autoClose: false,
          icon: false,
        });
        return;
      }

      // update session
      await update({
        ...session?.user,
        name: data.displayName, // eslint-disable-line @typescript-eslint/no-unsafe-assignment
        email: data.email, // eslint-disable-line @typescript-eslint/no-unsafe-assignment
        profile: data,
      });

      toast("Your profile has been updated", {
        type: "success",
        toastId: "patchUserProfile",
      });
    },
    [
      //setSubmittedValues,
      update,
      session,
    ]
  );

  return (
    <>
      <div className="container-centered">
        <div className="container-content">
          <h1 className="bold text-2xl underline">Settings</h1>
          <form
            onSubmit={handleSubmit(onSubmit)} // eslint-disable-line @typescript-eslint/no-misused-promises
            className="gap-2x flex flex-col"
          >
            <div className="form-control">
              <label className="label">
                <span className="label-text">Email</span>
              </label>
              <input
                type="text"
                className="input input-bordered w-full"
                {...register("email")}
              />
              {errors.email && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.email.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">First name</span>
              </label>
              <input
                type="text"
                className="input input-bordered"
                {...register("firstName")}
              />
              {errors.firstName && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.firstName.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">Last name</span>
              </label>
              <input
                type="text"
                className="input input-bordered"
                {...register("surname")}
              />
              {errors.surname && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.surname.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">Display name</span>
              </label>
              <input
                type="text"
                className="input input-bordered"
                {...register("displayName")}
              />
              {errors.displayName && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.displayName.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">Phone Number</span>
              </label>
              <input
                type="text"
                className="input input-bordered"
                {...register("phoneNumber")}
              />
              {errors.phoneNumber && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.phoneNumber.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">Country</span>
              </label>
              <select
                className="select select-bordered"
                {...register("countryId")}
              >
                <option value="">Please select</option>
                {countries?.map((country) => (
                  <option key={country.id} value={country.id}>
                    {country.name}
                  </option>
                ))}
              </select>
              {errors.countryId && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.countryId.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">Country Of Residence</span>
              </label>
              <select
                className="select select-bordered"
                {...register("countryOfResidenceId")}
              >
                <option value="">Please select</option>
                {countries?.map((country) => (
                  <option key={country.id} value={country.id}>
                    {country.name}
                  </option>
                ))}
              </select>
              {errors.countryOfResidenceId && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.countryOfResidenceId.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">Gender</span>
              </label>
              <select
                className="select select-bordered"
                {...register("genderId")}
              >
                <option value="">Please select</option>
                {genders?.map((item) => (
                  <option key={item.id} value={item.id}>
                    {item.name}
                  </option>
                ))}
              </select>
              {errors.genderId && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.genderId.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label">
                <span className="label-text">Date Of Birth</span>
              </label>
              <input
                type="date"
                className="input input-bordered"
                {...register("dateOfBirth")}
              />
              {errors.dateOfBirth && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.dateOfBirth.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="form-control">
              <label className="label cursor-pointer">
                <span className="label-text">Reset Password</span>
                <input
                  type="checkbox"
                  className="checkbox mr-2"
                  {...register("resetPassword")}
                />
              </label>
              {errors.resetPassword && (
                <label className="label">
                  <span className="label-text-alt italic text-red-500">
                    {/* eslint-disable-next-line @typescript-eslint/restrict-template-expressions */}
                    {`${errors.resetPassword.message}`}
                  </span>
                </label>
              )}
            </div>

            <div className="input-wrapper mt-4">
              <button
                type="submit"
                className="focus:shadow-outline rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700 focus:outline-none"
              >
                Submit
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

Settings.getLayout = function getLayout(page: ReactElement) {
  return <MainBackButtonLayout>{page}</MainBackButtonLayout>;
};

export default Settings;
