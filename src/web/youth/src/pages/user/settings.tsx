import { zodResolver } from "@hookform/resolvers/zod";
import { useCallback, useEffect, useState, type ReactElement } from "react";
import { useForm } from "react-hook-form";
import zod from "zod";
import { User } from "~/api/models/user";
import MainBackButtonLayout from "~/components/Layout/MainBackButton";
import { useCountries, useGenders } from "~/hooks/api/lookups";
import { useGetUser, usePatchUser } from "~/hooks/api/user";
import type { NextPageWithLayout } from "../_app";

const Settings: NextPageWithLayout = () => {
  const { data: genders } = useGenders();
  const { data: countries } = useCountries();
  const { data: user } = useGetUser();

  const [submittedValues, setSubmittedValues] = useState<User | null>(null);
  usePatchUser(submittedValues);

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
    country: zod.string().min(1, "Country is required"),
    countryOfResidence: zod.string().min(1, "Country of residence is required"),
    gender: zod.string().min(1, "Gender is required"),
    dateOfBirth: zod.coerce
      .date({
        required_error: "Please select a date and time",
        invalid_type_error: "That's not a date!",
      })
      .max(new Date(), { message: "Date of birth cannot be in the future" }),
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
    reset(user);
  }, [user]);

  // form submission handler
  const onSubmit = useCallback(
    async (data: any) => {
      setSubmittedValues(data);
    },
    [setSubmittedValues]
  );

  return (
    <>
      <div className="container-centered">
        <div className="container-content">
          <div className="m-auto flex flex-col gap-4">
            <h1 className="bold text-2xl underline">Settings</h1>
            <form
              onSubmit={handleSubmit(onSubmit)}
              className="gap-2x flex flex-col"
            >
              <div className="form-control">
                <label className="label">
                  <span className="label-text">Email</span>
                </label>
                <input
                  type="text"
                  className="input input-bordered"
                  {...register("email")}
                  // value={user?.email}
                />
                {errors.email && (
                  <label className="label">
                    <span className="label-text-alt italic text-red-500">
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
                  className="input input-bordered"
                  {...register("country")}
                >
                  <option value="">Please select</option>
                  {countries?.map((country) => (
                    <option key={country.id} value={country.id}>
                      {country.name}
                    </option>
                  ))}
                </select>
                {errors.country && (
                  <label className="label">
                    <span className="label-text-alt italic text-red-500">
                      {`${errors.country.message}`}
                    </span>
                  </label>
                )}
              </div>

              <div className="form-control">
                <label className="label">
                  <span className="label-text">Country Of Residence</span>
                </label>
                <select
                  className="input input-bordered"
                  {...register("countryOfResidence")}
                >
                  <option value="">Please select</option>
                  {countries?.map((country) => (
                    <option key={country.id} value={country.id}>
                      {country.name}
                    </option>
                  ))}
                </select>
                {errors.countryOfResidence && (
                  <label className="label">
                    <span className="label-text-alt italic text-red-500">
                      {`${errors.countryOfResidence.message}`}
                    </span>
                  </label>
                )}
              </div>

              <div className="form-control">
                <label className="label">
                  <span className="label-text">Gender</span>
                </label>
                <select
                  className="input input-bordered"
                  {...register("gender")}
                >
                  <option value="">Please select</option>
                  {genders?.map((item) => (
                    <option key={item.id} value={item.id}>
                      {item.name}
                    </option>
                  ))}
                </select>
                {errors.gender && (
                  <label className="label">
                    <span className="label-text-alt italic text-red-500">
                      {`${errors.gender.message}`}
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
                      {`${errors.dateOfBirth.message}`}
                    </span>
                  </label>
                )}
              </div>

              <div className="input-wrapper">
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
      </div>
    </>
  );
};

Settings.getLayout = function getLayout(page: ReactElement) {
  return <MainBackButtonLayout>{page}</MainBackButtonLayout>;
};

export default Settings;
